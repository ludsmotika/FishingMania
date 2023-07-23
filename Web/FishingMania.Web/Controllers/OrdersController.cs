namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Cart;
    using FishingMania.Web.ViewModels.Order;
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ICartsService cartsService;
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        public OrdersController(ICartsService cartsService, IProductsService productsService, IOrdersService ordersService)
        {
            this.cartsService = cartsService;
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        [HttpGet]
        public IActionResult MakeOrder(string shoppingCartId)
        {
            MakeOrderInputViewModel viewModel = new MakeOrderInputViewModel() { ShoppingCartId = shoppingCartId };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(MakeOrderInputViewModel orderViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(orderViewModel);
            }

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel shoppingCart = await this.cartsService.GetCartByUserIdAsync(currentUserId);

            if (shoppingCart == null)
            {
                throw new ArgumentException();
            }

            List<ProductInOrderViewModel> productsForOrder = await this.cartsService.GetProductsForShoppingCartByIdAsync(shoppingCart.Id);

            if (productsForOrder == null)
            {
                throw new ArgumentException();
            }

            string orderId = await this.ordersService.CreateOrderAsync(currentUserId, orderViewModel.Address);

            foreach (var productToAdd in productsForOrder)
            {
                await this.ordersService.AddProductToOrderAsync(productToAdd.ProductId, productToAdd.Amount, orderId);
                await this.productsService.DecreaseProductAmountAsync(productToAdd.ProductId, productToAdd.Amount);
                await this.cartsService.RemoveProductFromShoppingCartByIdAsync(productToAdd.Id);
            }

            await this.cartsService.DeleteByIdAsync(shoppingCart.Id);

            return this.RedirectToAction("MyOrders", "ApplicationUser");
        }
    }
}
