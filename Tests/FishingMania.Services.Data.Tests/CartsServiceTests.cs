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
    using FishingMania.Web.ViewModels.Cart;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

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
               .UseInMemoryDatabase("FishingManiaCarts")
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
        public async Task GetCartByUserIdAsyncWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(ShoppingCartViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var cart = await this.cartsService.GetCartByUserIdAsync("testUserId");

            Assert.NotNull(cart);
        }

        [Fact]
        public async Task DoesAddingProductToCartByIdsWorkCorrect()
        {
            this.SeedDataAsync();

            var result = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);

            Assert.Equal("addedSuccessfully", result);
        }

        [Fact]
        public async Task DoesAddingProductAgainWorkCorrect()
        {
            this.SeedDataAsync();

            var firstAddResult = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);
            var secondAddResult = await this.cartsService.AddProductToCartByIds(2, "testUserId", 1);

            Assert.Equal("addedSuccessfully", firstAddResult);
            Assert.Equal("alreadyAdded", secondAddResult);
        }

        [Fact]
        public async Task DoesAddingProductWithoutAmountInStocksWorkCorrect()
        {
            this.SeedDataAsync();

            var result = await this.cartsService.AddProductToCartByIds(3, "testUserId", 1);

            Assert.Equal("insufficientQuantity", result);
        }

        [Fact]
        public async Task CreateCartForUserByIdAsyncWorkCorrect()
        {
            this.SeedDataAsync();

            await this.cartsService.CreateCartForUserByIdAsync("userId");

            bool isCreated = await this.applicationDbContext.ShoppingCarts.AnyAsync(sc => sc.ApplicationUserId == "userId");

            Assert.True(isCreated == true);
        }

        [Fact]
        public async Task DeleteByIdAsyncWorkCorrect()
        {
            this.SeedDataAsync();

            await this.cartsService.DeleteByIdAsync("1");

            bool isDeleted = !await this.applicationDbContext.ShoppingCarts.AnyAsync(sc => sc.Id == "1");

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DoesUserHasCartReturnTrue()
        {
            this.SeedDataAsync();

            bool hasCart = await this.cartsService.DoesUserHasCartAsync("testUserId");

            Assert.True(hasCart == true);
        }

        [Fact]
        public async Task DoesUserHasCartReturnFalse()
        {
            this.SeedDataAsync();

            bool hasCart = await this.cartsService.DoesUserHasCartAsync("testUserId2");

            Assert.True(hasCart == false);
        }

        [Fact]
        public async Task RemoveProductFromShoppingCartByIdAsyncWithValidId()
        {
            this.SeedDataAsync();

            await this.cartsService.RemoveProductFromShoppingCartByIdAsync("test");

            bool isRemoved = !await this.applicationDbContext.ShoppingCartProducts.AnyAsync(scp => scp.Id == "test");

            Assert.True(isRemoved);
        }

        [Fact]
        public async Task RemoveProductFromShoppingCartByIdAsyncThrowsArgumentExceptionWithInvalidId()
        {
            this.SeedDataAsync();

            async Task Remove() => await this.cartsService.RemoveProductFromShoppingCartByIdAsync("test1");

            await Assert.ThrowsAsync<ArgumentException>(Remove);
        }

        [Fact]
        public async Task DoesProductIsInShoppingCartAsyncWorksWithAddedProduct()
        {
            this.SeedDataAsync();

            bool isInCart = await this.cartsService.DoesProductIsInShoppingCartAsync("1", 1, "test");

            Assert.True(isInCart == true);
        }

        [Fact]
        public async Task DoesProductIsInShoppingCartAsyncWorksWithNotAddedProduct()
        {
            this.SeedDataAsync();

            bool isInCart = await this.cartsService.DoesProductIsInShoppingCartAsync("1", 2, "test");

            Assert.True(isInCart == false);
        }

        [Fact]
        public async Task DoesProductIsInShoppingCartAsyncWorksWithAddedProductButHaveInvalidId()
        {
            this.SeedDataAsync();

            bool isInCart = await this.cartsService.DoesProductIsInShoppingCartAsync("1", 1, "test1");

            Assert.True(isInCart == false);
        }

        [Fact]
        public async Task GetProductsForShoppingCartByIdAsyncWorkCorrect()
        {
            this.SeedDataAsync();

            var products = await this.cartsService.GetProductsForShoppingCartByIdAsync("1");

            Assert.Single(products);
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

            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
                Images = new List<Image> { firstImage, secondImage },
            };

            var productToAdd = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
                Images = new List<Image> { firstImage, secondImage },
            };

            var productWithoutAmount = new Product()
            {
                Id = 3,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 0,
                Images = new List<Image> { firstImage, secondImage },
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.AddAsync(productToAdd);
            await this.productsRepository.AddAsync(productWithoutAmount);
            await this.productsRepository.SaveChangesAsync();

            var cart = new ShoppingCart()
            {
                Id = "1",
                ApplicationUserId = "testUserId",
                ApplicationUser = applicationUser,
            };

            await this.shoppingCartsRepository.AddAsync(cart);
            await this.shoppingCartsRepository.SaveChangesAsync();

            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Id = "test",
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
