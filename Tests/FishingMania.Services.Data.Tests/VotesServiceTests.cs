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
using FishingMania.Web.ViewModels.Votes;
using System.Threading;

namespace FishingMania.Services.Data.Tests
{
    public class VotesServiceTests
    {

        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Vote> votesRepository;

        private VotesService votesService;

        public VotesServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaVotes")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.votesRepository = new EfDeletableEntityRepository<Vote>(this.applicationDbContext);
            this.votesService = new VotesService(this.votesRepository);
        }

        [Fact]
        public async Task DoesVoteExistAsyncWorksCorrectWithValidId()
        {
            this.SeedDataAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId1",
                IsPositive = true,
                IsClicked = true,
                CatchId = 1,
            };

            var doesExist = await this.votesService.DoesVoteExistAsync(viewModel);

            Assert.True(doesExist);
        }

        [Fact]
        public async Task DeleteVoteAsyncWorksRight()
        {
            this.SeedDataAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId1",
                IsPositive = true,
                IsClicked = true,
                CatchId = 1,
            };

            await this.votesService.DeleteVoteAsync(viewModel);

            Assert.Equal(0, this.votesRepository.All().Count());
        }

        [Fact]
        public async Task DoesVoteExistAsyncWorksCorrectWithInvalidId()
        {
            this.SeedDataAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId1",
                IsPositive = true,
                IsClicked = true,
                CatchId = 2,
            };

            var doesExist = await this.votesService.DoesVoteExistAsync(viewModel);

            Assert.True(doesExist == false);
        }

        [Fact]
        public async Task VotePostAsyncWorksCorrect()
        {
            this.SeedDataAsync();

            var secondApplicationUser = new ApplicationUser()
            {
                Id = "testUserId2",
                Email = "testemail2@abv.bg",
                NormalizedEmail = "TESTEMAIL2@ABV.Bg",
                UserName = "testUser2",
                NormalizedUserName = "TESTUSER2",
                SecurityStamp = "123456",
                PasswordHash = "123456",
                ConcurrencyStamp = "123456",
                EmailConfirmed = false,
            };

            await this.applicationDbContext.Users.AddAsync(secondApplicationUser);
            await this.applicationDbContext.SaveChangesAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId2",
                IsPositive = true,
                IsClicked = true,
                CatchId = 1,
            };

            await this.votesService.VotePostAsync(viewModel);

            Assert.Equal(2, this.votesRepository.All().Count());
        }

        [Fact]
        public async Task DoesVoteExistWithOppositeEmotionWorksWithValidInputModel()
        {
            this.SeedDataAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId1",
                IsPositive = false,
                IsClicked = true,
                CatchId = 1,
            };

            var doesExist = await this.votesService.DoesVoteExistWithOppositeEmotion(viewModel);

            Assert.True(doesExist);
        }

        [Fact]
        public async Task DoesVoteExistWithOppositeEmotionWorksWithInvalidInputModel()
        {
            this.SeedDataAsync();

            VoteInputViewModel viewModel = new VoteInputViewModel()
            {
                ApplicationUserId = "testUserId1",
                IsPositive = true,
                IsClicked = true,
                CatchId = 1,
            };

            var doesExist = await this.votesService.DoesVoteExistWithOppositeEmotion(viewModel);

            Assert.True(doesExist == false);
        }

        private async Task SeedDataAsync()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = "testUserId1",
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
                ApplicationUserId = "testUserId1",
                ApplicationUser = applicationUser,
                ImageId = 1,
                Image = imageForFirstCatch,
                FishingSpotId = 1,
                FishingSpot = fishingSpot,
                FishSpeciesId = 1,
                FishSpecies = fishSpecies,
            };

            var firstVote = new Vote()
            {
                Id = 1,
                ApplicationUser = applicationUser,
                ApplicationUserId = "testUserId1",
                CatchId = 1,
                Catch = firstCatch,
                IsPositive = true,
            };

            await this.votesRepository.AddAsync(firstVote);
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
