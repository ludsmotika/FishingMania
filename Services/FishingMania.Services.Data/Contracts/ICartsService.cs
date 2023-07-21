namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Cart;

    public interface ICartsService
    {
        Task<ShoppingCartViewModel> GetCartByUserIdAsync(string id);

        Task<bool> DoesUserHasCartAsync(string id);

        Task CreateCartForUserByIdAsync(string id);

        Task<bool> AddProductToCartByIds(int productId, string userId, int amount);
    }
}
