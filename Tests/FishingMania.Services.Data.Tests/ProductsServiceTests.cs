namespace FishingMania.Services.Data.Tests
{
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
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProductsServiceTests
    {

        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Product> productsRepository;

        private ProductsService productsService;

        public ProductsServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaProducts")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.productsRepository = new EfDeletableEntityRepository<Product>(this.applicationDbContext);
            this.productsService = new ProductsService(this.productsRepository);
        }

        [Fact]
        public async Task DecreaseProductAmountAsyncWorksCorrect()
        {
            this.SeedDataAsync();

            await this.productsService.DecreaseProductAmountAsync(1, 1);

            Assert.Equal(1, this.productsRepository.All().Where(p => p.Id == 1).FirstOrDefault().AmountInStock);
        }

        [Fact]
        public async Task DoesProductExistByIdAsyncWorksCorrectWithValidId()
        {
            this.SeedDataAsync();

            var doesExist = await this.productsService.DoesProductExistByIdAsync(1);

            Assert.True(doesExist);
        }

        [Fact]
        public async Task DoesProductExistByIdAsyncWorksCorrectWithInvalidId()
        {
            this.SeedDataAsync();

            var doesExist = await this.productsService.DoesProductExistByIdAsync(3);

            Assert.True(doesExist == false);
        }

        [Fact]
        public async Task GetAllProductsAsyncWorksCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ProductViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            AllProductsQueryViewModel allProductsQueryViewModel = new AllProductsQueryViewModel()
            {
                CurrentPage = -1,
                ProductsPerPage = 6,
                TotalProducts = 2,
                SearchString = "Test",
                SelectedCategoryId = 1,
            };

            var products = await this.productsService.GetAllProductsAsync(allProductsQueryViewModel);

            Assert.Equal(2, products.Products.Count());
        }

        [Fact]
        public async Task GetProductByIdAsyncWorksWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(ProductDetailsViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var product = await this.productsService.GetProductByIdAsync(1);

            Assert.NotNull(product);
        }

        private async Task SeedDataAsync()
        {
            var firstImage = new Image()
            {
                Id = 1,
                URL = "firstImageUrl",
            };
            var secondImage = new Image()
            {
                Id = 2,
                URL = "secondImageUrl",
            };

            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Test",
                Country = "Test",
            };

            var productCategory = new ProductCategory()
            {
                Id = 1,
                Name = "Test",
            };

            var firstProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 1m,
                AmountInStock = 2,
                Images = new List<Image> { firstImage, secondImage },
                ManufacturerId = 1,
                Manufacturer = manufacturer,
                CategoryId = 1,
                ProductCategory = productCategory,
            };

            var secondProduct = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                Price = 1m,
                AmountInStock = 1,
                Images = new List<Image> { firstImage, secondImage },
                ManufacturerId = 1,
                Manufacturer = manufacturer,
                CategoryId = 1,
                ProductCategory = productCategory,
            };

            await this.productsRepository.AddAsync(firstProduct);
            await this.productsRepository.AddAsync(secondProduct);

            await this.productsRepository.SaveChangesAsync();
        }
    }
}
