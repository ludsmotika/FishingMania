using AutoMapper.Configuration.Annotations;
using FishingMania.Data;
using FishingMania.Data.Common.Repositories;
using FishingMania.Data.Models;
using FishingMania.Data.Repositories;
using FishingMania.Services.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FishingMania.Services.Data.Tests
{
    public class CartsServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<ShoppingCart> shoppingCartsRepository;
        private IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private IDeletableEntityRepository<Product> productsRepository;

        private CartsService cartsService;

        public CartsServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingMania")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.productsRepository = new EfDeletableEntityRepository<Product>(this.applicationDbContext);
            this.shoppingCartProductsRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(this.applicationDbContext);
            this.shoppingCartsRepository = new EfDeletableEntityRepository<ShoppingCart>(this.applicationDbContext);
            this.cartsService = new CartsService(this.shoppingCartsRepository, this.shoppingCartProductsRepository, this.productsRepository);
        }

        [Fact]
        public async Task DoesAddingProductToCartByIdsWorkCorrect()
        {
            await this.SeedDataAsync();

            var result = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);

            Assert.Equal("addedSuccessfully", result);
        }

        [Fact]
        public async Task DoesAddingProductAgainWorkCorrect()
        {
            await this.SeedDataAsync();

            var firstAddResult = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);
            var secondAddResult = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);

            Assert.Equal("addedSuccessfully", firstAddResult);
            Assert.Equal("alreadyAdded", secondAddResult);
        }

        [Fact]
        public async Task DoesAddingProductWithoutAmountInStocksWorkCorrect()
        {
            await this.SeedDataAsync();

            var result = await this.cartsService.AddProductToCartByIds(3, "testUserId", 1);

            Assert.Equal("insufficientQuantity", result);
        }

        private async Task SeedDataAsync()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
            };

            var productToAdd = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
            };

            var productWithoutAmount = new Product()
            {
                Id = 3,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 0,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.AddAsync(productToAdd);
            await this.productsRepository.AddAsync(productWithoutAmount);
            await this.productsRepository.SaveChangesAsync();

            var cart = new ShoppingCart()
            {
                Id = "1",
                ApplicationUserId = "testUserId",
            };

            await this.shoppingCartsRepository.AddAsync(cart);
            await this.shoppingCartsRepository.SaveChangesAsync();

            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Amount = 1,
                ShoppingCart = cart,
                ShoppingCartId = "1",
                Product = product,
                ProductId = 1,
            };
            await this.shoppingCartProductsRepository.AddAsync(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();

            cart.ShoppingCartProducts.ToList<ShoppingCartProduct>().Add(shoppingCartProduct);
            product.ShoppingCartProduct.ToList<ShoppingCartProduct>().Add(shoppingCartProduct);

            await this.shoppingCartsRepository.SaveChangesAsync();
            await this.productsRepository.SaveChangesAsync();
        }
    }
}
