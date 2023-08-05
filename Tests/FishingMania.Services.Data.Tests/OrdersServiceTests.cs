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
    using FishingMania.Web.ViewModels.Order;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Order> ordersRepository;
        private IDeletableEntityRepository<OrderProduct> orderProductsRepository;

        private OrdersService ordersService;

        public OrdersServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaOrders")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.ordersRepository = new EfDeletableEntityRepository<Order>(this.applicationDbContext);
            this.orderProductsRepository = new EfDeletableEntityRepository<OrderProduct>(this.applicationDbContext);
            this.ordersService = new OrdersService(this.ordersRepository, this.orderProductsRepository);
        }

        [Fact]
        public async Task GetOrdersForUserByIdAsyncWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(OrderViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var orders = await this.ordersService.GetOrdersForUserByIsAsync("testUserId");

            Assert.Single(orders);
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
                URL = "1111",
            };
            var secondImage = new Image()
            {
                Id = 2,
                URL = "2222",
            };

            var firstProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
                Images = new List<Image>() { firstImage, secondImage },
            };

            var secondProduct = new Product()
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
                Images = new List<Image>() { firstImage, secondImage },
            };

            var thirdProduct = new Product()
            {
                Id = 3,
                Name = "Test",
                Description = "Test",
                Price = 1,
                AmountInStock = 1,
                Images = new List<Image>() { firstImage, secondImage },
            };

            await this.applicationDbContext.Products.AddAsync(thirdProduct);

            var order = new Order()
            {
                Id = "Test1",
                ApplicationUserId = "testUserId",
                ApplicationUser = applicationUser,
                Address = "Test Address",
            };

            var firstOrderProduct = new OrderProduct()
            {
                Id = "Test1",
                OrderId = "Test1",
                Order = order,
                ProductId = 1,
                Product = firstProduct,
                Amount = 1,
            };

            var secondOrderProduct = new OrderProduct()
            {
                Id = "Test2",
                OrderId = "Test1",
                Order = order,
                ProductId = 2,
                Product = secondProduct,
                Amount = 1,
            };

            await this.orderProductsRepository.AddAsync(firstOrderProduct);
            await this.orderProductsRepository.AddAsync(secondOrderProduct);
            await this.orderProductsRepository.SaveChangesAsync();

            order.OrderProducts = new List<OrderProduct>() { firstOrderProduct, secondOrderProduct };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
