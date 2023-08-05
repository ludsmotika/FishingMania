namespace FishingMania.Services.Data.Tests
{
    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Data.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ReportsServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Report> reportsRepository;

        private ReportsService reportsService;

        public ReportsServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaReports")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.reportsRepository = new EfDeletableEntityRepository<Report>(this.applicationDbContext);
            this.reportsService = new ReportsService(this.reportsRepository);
        }

        [Fact]
        public async Task ReportCatchWorksCorrect()
        {
            this.SeedDataAsync();

            await this.reportsService.ReportCatch(1, "testUserId", "Test Complain 2");

            Assert.Equal(2, this.reportsRepository.All().Count());
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

            var firstReport = new Report()
            {
                Id = Guid.NewGuid(),
                CatchId = 1,
                Catch = firstCatch,
                ApplicationUserId = "testUserId",
                ApplicationUser = applicationUser,
                Complain = "Test Complain",
            };

            await this.reportsRepository.AddAsync(firstReport);
            await this.reportsRepository.SaveChangesAsync();
        }
    }
}
