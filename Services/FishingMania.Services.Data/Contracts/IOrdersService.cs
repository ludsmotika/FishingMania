namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Order;

    public interface IOrdersService
    {
        Task<string> CreateOrderAsync(string userId, string address);

        Task AddProductToOrderAsync(int productId, int amount, string orderId);

        Task<List<OrderViewModel>> GetOrdersForUserByIsAsync(string userId);
    }
}
