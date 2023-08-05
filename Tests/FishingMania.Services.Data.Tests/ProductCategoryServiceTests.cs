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
    using FishingMania.Web.ViewModels.ProductCategory;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProductCategoryServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<ProductCategory> productCategoriesRepository;

        private ProductCategoryService productCategoryService;

        public ProductCategoryServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaProductsCategories")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.productCategoriesRepository = new EfDeletableEntityRepository<ProductCategory>(this.applicationDbContext);
            this.productCategoryService = new ProductCategoryService(this.productCategoriesRepository);
        }

        [Fact]
        public async Task GetAllProductCategoriesAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ProductCategoryViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var productCategories = await this.productCategoryService.GetAllProductCategoriesAsync();

            Assert.Equal(2, productCategories.Count);
        }

        private async Task SeedDataAsync()
        {
            var firstProductCategory = new ProductCategory()
            {
                Id = 1,
                Name = "Test1",
            };

            var secondProductCategory = new ProductCategory()
            {
                Id = 2,
                Name = "Test2",
            };

            await this.productCategoriesRepository.AddAsync(firstProductCategory);
            await this.productCategoriesRepository.AddAsync(secondProductCategory);

            await this.productCategoriesRepository.SaveChangesAsync();
        }
    }
}
