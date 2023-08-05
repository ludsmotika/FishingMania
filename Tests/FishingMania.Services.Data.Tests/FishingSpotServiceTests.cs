namespace FishingMania.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Services.Data.Services;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FishingSpotServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<FishingSpot> fishingSpotsRepository;

        private FishingSpotService fishingSpotService;

        public FishingSpotServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaFishingSpots")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.fishingSpotsRepository = new EfDeletableEntityRepository<FishingSpot>(this.applicationDbContext);
            this.fishingSpotService = new FishingSpotService(this.fishingSpotsRepository);
        }

        [Fact]
        public async Task GetFishSpeciesForSpotByIdAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishSpeciesDropdownViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishSpeciesForSpot = await this.fishingSpotService.GetFishSpeciesForSpotByIdAsync(1);

            Assert.Single(fishSpeciesForSpot);
        }

        [Fact]
        public async Task GetCountAsyncWorkCorrect()
        {
            this.SeedDataAsync();

            var count = await this.fishingSpotService.GetCountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task FishingSpotHasFishSpeciesAsyncWithValidIds()
        {
            this.SeedDataAsync();

            bool hasFishSpecies = await this.fishingSpotService.FishingSpotHasFishSpeciesAsync(1, 1);

            Assert.True(hasFishSpecies);
        }

        [Fact]
        public async Task GetCountByTypeAsyncWithValidInput()
        {
            this.SeedDataAsync();

            var count = await this.fishingSpotService.GetCountByTypeAsync(FishingSpotType.River);

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetCountByTypeAsyncWithInvalidInput()
        {
            this.SeedDataAsync();

            var count = await this.fishingSpotService.GetCountByTypeAsync(FishingSpotType.Swamp);

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task GetSpotForDetailsByIdAsyncWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishingSpotDetailsViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishingSpot = await this.fishingSpotService.GetSpotForDetailsByIdAsync(1);

            Assert.NotNull(fishingSpot);
        }

        [Fact]
        public async Task GetSpotForDetailsByIdAsyncWithInvalidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishingSpotDetailsViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishingSpot = await this.fishingSpotService.GetSpotForDetailsByIdAsync(2);

            Assert.Null(fishingSpot);
        }

        [Fact]
        public async Task AllForInputAsyncWorkCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishingSpotDropdownViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishingSpots = await this.fishingSpotService.AllForInputAsync();

            Assert.NotNull(fishingSpots);

            Assert.Single(fishingSpots);
        }

        [Fact]
        public async Task GetAllFishingSpotsAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishingSpotViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishingSpots = await this.fishingSpotService.GetAllFishingSpotsAsync(1, 6);

            Assert.NotNull(fishingSpots);
            Assert.Single(fishingSpots);
        }

        [Fact]
        public async Task GetAllFishingSpotsByTypeAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishingSpotViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(FishingSpotType.River, 1, 6);

            Assert.NotNull(fishingSpots);
            Assert.Single(fishingSpots);
        }

        private async Task SeedDataAsync()
        {
            var imageForSpot = new Image()
            {
                Id = 1,
                URL = "secondImageUrl",
            };
            var imageForFishSpecies = new Image()
            {
                Id = 2,
                URL = "thirdImageUrl",
            };

            var fishSpecies = new FishSpecies()
            {
                Id = 1,
                Name = "Test",
                ImageId = 2,
                Image = imageForFishSpecies,
            };

            var fishingSpot = new FishingSpot()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Latitude = 11.11m,
                Longitude = 22.22m,
                FishingSpotType = FishingSpotType.River,
                FishSpecies = new List<FishSpecies>() { fishSpecies },
                ImageId = 1,
                Image = imageForSpot,
            };

            await this.fishingSpotsRepository.AddAsync(fishingSpot);
            await this.fishingSpotsRepository.SaveChangesAsync();
        }
    }
}
