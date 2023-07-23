namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Cart;

    public interface ICartsService
    {
        Task<ShoppingCartViewModel> GetCartByUserIdAsync(string id);

        Task<bool> DoesUserHasCartAsync(string id);

        Task CreateCartForUserByIdAsync(string id);

        Task<string> AddProductToCartByIds(int productId, string userId, int amount);

        Task RemoveProductFromShoppingCartByIdAsync(string shoppingCartProductId);

        Task<bool> DoesProductIsInShoppingCartAsync(string shoppingCartId, int productId, string shoppingCartProductId);
    }
}
