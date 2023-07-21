namespace FishingMania.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Cart;
    using Microsoft.EntityFrameworkCore;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartsRepository;
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public CartsService(IDeletableEntityRepository<ShoppingCart> shoppingCartsRepository, IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository, IDeletableEntityRepository<Product> productsRepository)
        {
            this.shoppingCartsRepository = shoppingCartsRepository;
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.productsRepository = productsRepository;
        }

        public async Task<bool> AddProductToCartByIds(int productId, string userId, int amount)
        {
            ShoppingCart shoppingCart = await this.shoppingCartsRepository.All().Where(sc => sc.ApplicationUserId == userId).FirstOrDefaultAsync();

            bool productAlreadyAdded = await this.shoppingCartProductsRepository.All().Where(sc => sc.ShoppingCartId == shoppingCart.Id && sc.ProductId == productId).AnyAsync();
            if (productAlreadyAdded)
            {
                return false;
            }

            Product product = await this.productsRepository.All().Where(p => p.Id == productId).FirstOrDefaultAsync();
            if (product.AmountInStock < amount)
            {
                return false;
            }

            ShoppingCartProduct shoppingCartProductToAdd = new ShoppingCartProduct()
            {
                ProductId = productId,
                ShoppingCartId = shoppingCart.Id,
                Amount = amount,
            };

            await this.shoppingCartProductsRepository.AddAsync(shoppingCartProductToAdd);
            await this.shoppingCartProductsRepository.SaveChangesAsync();

            return true;
        }

        public async Task CreateCartForUserByIdAsync(string id)
        {
            await this.shoppingCartsRepository.AddAsync(new ShoppingCart() { ApplicationUserId = id });
            await this.shoppingCartsRepository.SaveChangesAsync();
        }

        public async Task<bool> DoesUserHasCartAsync(string id)
        {
            return await this.shoppingCartsRepository.All().Where(sc => sc.ApplicationUserId == id).AnyAsync();
        }

        public async Task<ShoppingCartViewModel> GetCartByUserIdAsync(string id)
        {
            ShoppingCartViewModel shoppingCartViewModel = await this.shoppingCartsRepository
                                                                    .All()
                                                                    .Where(sc => sc.ApplicationUserId == id)
                                                                    .Include(sc => sc.ApplicationUser)
                                                                    .Include(sc => sc.ShoppingCartProducts)
                                                                    .ThenInclude(scp => scp.Product)
                                                                    .ThenInclude(p => p.Images)
                                                                    .Select(sc => new ShoppingCartViewModel()
                                                                    {
                                                                        Id = sc.Id,
                                                                        ApplicationUserId = sc.ApplicationUserId,
                                                                        ApplicationUser = sc.ApplicationUser,
                                                                        ShoppingCartProducts = sc.ShoppingCartProducts.ToList(),
                                                                    })
                                                                    .FirstOrDefaultAsync();

            return shoppingCartViewModel;
        }
    }
}
