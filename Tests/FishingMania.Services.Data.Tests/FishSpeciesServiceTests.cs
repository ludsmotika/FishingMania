namespace FishingMania.Services.Data.Tests
{
    using System.Reflection;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Services.Data.Services;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FishSpeciesServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<FishSpecies> fishSpeciesRepository;

        private FishSpeciesService fishSpeciesService;

        public FishSpeciesServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaFishSpecies")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.fishSpeciesRepository = new EfDeletableEntityRepository<FishSpecies>(this.applicationDbContext);
            this.fishSpeciesService = new FishSpeciesService(this.fishSpeciesRepository);
        }

        [Fact]
        public async Task AllForInputAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(FishSpeciesDropdownViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var fishSpecies = await this.fishSpeciesService.AllForInputAsync();

            Assert.Equal(2, fishSpecies.Count);
        }

        private async Task SeedDataAsync()
        {

            var imageForFirstFishSpecies = new Image()
            {
                Id = 1,
                URL = "Test1",
            };

            var firstFishSpecies = new FishSpecies()
            {
                Id = 1,
                Name = "Test1",
                Type = FishType.Carnivorous,
                ImageId = 1,
                Image = imageForFirstFishSpecies,
            };

            var imageForSecondFishSpecies = new Image()
            {
                Id = 2,
                URL = "Test2",
            };

            var secondFishSpecies = new FishSpecies()
            {
                Id = 2,
                Name = "Test2",
                Type = FishType.Carnivorous,
                ImageId = 2,
                Image = imageForSecondFishSpecies,
            };

            await this.fishSpeciesRepository.AddAsync(firstFishSpecies);
            await this.fishSpeciesRepository.AddAsync(secondFishSpecies);
            await this.fishSpeciesRepository.SaveChangesAsync();
        }
    }
}
