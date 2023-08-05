namespace FishingMania.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Services.Data.Services;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Catch.Enums;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CatchesServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Catch> catchesRepository;

        private CatchesService catchesService;
        private ImageService imageService;

        public CatchesServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaCatches")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.catchesRepository = new EfDeletableEntityRepository<Catch>(this.applicationDbContext);
            this.catchesService = new CatchesService(this.catchesRepository, this.imageService);
        }

        [Fact]
        public async Task GetCatchByIdAsyncWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(CatchDetailsViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            CatchDetailsViewModel resultCatch = await this.catchesService.GetCatchByIdAsync(1);

            Assert.NotNull(resultCatch);
        }

        [Fact]
        public async Task DoesCatchExistWithValidId()
        {
            this.SeedDataAsync();

            bool resultCatch = await this.catchesService.DoesCatchExist(1);

            Assert.True(resultCatch);
        }

        [Fact]
        public async Task DoesCatchExistWithInvalidId()
        {
            this.SeedDataAsync();

            bool resultCatch = await this.catchesService.DoesCatchExist(2);

            Assert.True(!resultCatch);
        }

        [Fact]
        public async Task GetCatchesByUserIdAsyncWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(CatchViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var catches = await this.catchesService.GetCatchesByUserIdAsync("testUserId");

            Assert.NotNull(catches);
            Assert.Single(catches);
        }

        [Fact]
        public async Task DeleteByIdAsyncWorksWithValidId()
        {
            this.SeedDataAsync();

            await this.catchesService.DeleteByIdAsync(1);

            bool isDeleted = !await this.catchesRepository.All().Where(x => x.Id == 1).AnyAsync();

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteByIdAsyncWorksWithInvalidId()
        {
            this.SeedDataAsync();

            async Task Remove() => await this.catchesService.DeleteByIdAsync(2);

            await Assert.ThrowsAsync<ArgumentException>(Remove);
        }

        [Fact]
        public async Task GetAllCatchesAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(CatchViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            AllCatchesQueryViewModel allCatchesQueryViewModel = new AllCatchesQueryViewModel()
            {
                CurrentPage = -1,
                CatchesPerPage = 6,
                CatchesSorting = CatchesSorting.Oldest,
                SearchString = "Test",
                Type = FishType.Omnivorous,
                TotalCatches = 1,
            };

            var catches = await this.catchesService.GetAllCatchesAsync(allCatchesQueryViewModel);

            Assert.Single(catches.Catches);
            Assert.Equal("Test", catches.Catches.First().Description);
            Assert.Equal(1, catches.Catches.First().Id);
            Assert.Equal(1.95m, catches.Catches.First().FishWeight);
        }

        private async Task SeedDataAsync()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = "testUserId",
                Email = "testemail@abv.bg",
                NormalizedEmail = "TESTEMAIL@ABV.Bg",
                UserName = "testUser",
                NormalizedUserName = "TESTUSER",
                SecurityStamp = "12345",
                PasswordHash = "12345",
                ConcurrencyStamp = "12345",
                EmailConfirmed = false,
            };

            var imageForFirstCatch = new Image()
            {
                Id = 1,
                URL = "firstImageUrl",
            };
            var imageForSpot = new Image()
            {
                Id = 2,
                URL = "secondImageUrl",
            };
            var imageForFishSpecies = new Image()
            {
                Id = 3,
                URL = "thirdImageUrl",
            };

            var fishSpecies = new FishSpecies()
            {
                Id = 1,
                Name = "Test",
                ImageId = 3,
                Image = imageForFishSpecies,
                Type = FishType.Omnivorous,
            };

            var fishingSpot = new FishingSpot()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Latitude = 11.11m,
                Longitude = 22.22m,
                FishSpecies = new List<FishSpecies>() { fishSpecies },
                ImageId = 2,
                Image = imageForSpot,
            };

            var firstCatch = new Catch()
            {
                Id = 1,
                Description = "Test",
                FishWeight = 1.95m,
                ApplicationUserId = "testUserId",
                ApplicationUser = applicationUser,
                ImageId = 1,
                Image = imageForFirstCatch,
                FishingSpotId = 1,
                FishingSpot = fishingSpot,
                FishSpeciesId = 1,
                FishSpecies = fishSpecies,
            };

            await this.catchesRepository.AddAsync(firstCatch);
            await this.catchesRepository.SaveChangesAsync();
        }
    }
}
