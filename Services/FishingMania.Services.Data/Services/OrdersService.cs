namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Order;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderProduct> orderProductsRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository, IDeletableEntityRepository<OrderProduct> orderProductsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.orderProductsRepository = orderProductsRepository;
        }

        public async Task AddProductToOrderAsync(int productId, int amount, string orderId)
        {
            OrderProduct orderProduct = new OrderProduct { ProductId = productId, Amount = amount, OrderId = orderId };
            await this.orderProductsRepository.AddAsync(orderProduct);
            await this.orderProductsRepository.SaveChangesAsync();
        }

        public async Task<string> CreateOrderAsync(string userId, string address)
        {
            Order orderToAdd = new Order()
            {
                ApplicationUserId = userId,
                Address = address,
                CreatedOn = DateTime.Now,
            };

            await this.ordersRepository.AddAsync(orderToAdd);
            await this.ordersRepository.SaveChangesAsync();

            return orderToAdd.Id;
        }

        public async Task<List<OrderViewModel>> GetOrdersForUserByIsAsync(string userId)
        {
            return await this.ordersRepository.All().Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Images).Where(o => o.ApplicationUserId == userId).To<OrderViewModel>().ToListAsync();
        }
    }
}
