using FishingMania.Data.Common.Repositories;
using FishingMania.Data.Repositories;
using FishingMania.Data;
using FishingMania.Services.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishingMania.Data.Models;
using Xunit;
using FishingMania.Services.Mapping;
using FishingMania.Web.ViewModels.Comment;
using System.Reflection;
using AutoMapper.Configuration.Annotations;

namespace FishingMania.Services.Data.Tests
{
    public class FishingSpotServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<FishingSpot> fishingSpotsRepository;

        private FishingSpotService fishingSpotService;

        public FishingSpotServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingMania")
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
            AutoMapperConfig.RegisterMappings(typeof(CommentViewModel).GetTypeInfo().Assembly);

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
                FishSpecies = new List<FishSpecies>() { fishSpecies },
                ImageId = 1,
                Image = imageForSpot,
            };

            await this.fishingSpotsRepository.AddAsync(fishingSpot);
            await this.fishingSpotsRepository.SaveChangesAsync();
        }
    }
}
