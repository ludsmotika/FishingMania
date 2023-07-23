namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Cart;
    using FishingMania.Web.ViewModels.Product;

    public interface ICartsService
    {
        Task<ShoppingCartViewModel> GetCartByUserIdAsync(string id);

        Task<bool> DoesUserHasCartAsync(string id);

        Task CreateCartForUserByIdAsync(string id);

        Task DeleteByIdAsync(string id);

        Task<string> AddProductToCartByIds(int productId, string userId, int amount);

        Task RemoveProductFromShoppingCartByIdAsync(string shoppingCartProductId);

        Task<bool> DoesProductIsInShoppingCartAsync(string shoppingCartId, int productId, string shoppingCartProductId);

        Task<List<ProductInOrderViewModel>> GetProductsForShoppingCartByIdAsync(string shoppingCartId);
    }
}
