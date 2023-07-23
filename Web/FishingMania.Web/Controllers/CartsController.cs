namespace FishingMania.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;
        private readonly IProductsService productsService;

        public CartsController(ICartsService cartsService, IProductsService productsService)
        {
            this.cartsService = cartsService;
            this.productsService = productsService;
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("AddProductToCartAsync")]
        [ActionName("AddProductToCartAsync")]
        public async Task<IActionResult> AddProductToCartAsync(ProductAddToCartViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                if (!await this.cartsService.DoesUserHasCartAsync(model.ApplicationUserId))
                {
                    await this.cartsService.CreateCartForUserByIdAsync(model.ApplicationUserId);
                }

                string queryStatus = await this.cartsService.AddProductToCartByIds(model.ProductId, model.ApplicationUserId, model.Amount);

                return this.Json(queryStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [HttpPost("RemoveProductFromCartAsync")]
        [ActionName("RemoveProductFromCartAsync")]
        public async Task RemoveProductFromCartAsync([FromForm] int productId, [FromForm] string shoppingCartProductId, [FromForm] string shoppingCartId)
        {
            try
            {
                bool doesProductExist = await this.productsService.DoesProductExistByIdAsync(productId);

                bool doesProductIsInShoopingCart = await this.cartsService.DoesProductIsInShoppingCartAsync(shoppingCartId, productId, shoppingCartProductId);

                if (!doesProductExist || !doesProductIsInShoopingCart)
                {
                    throw new ArgumentException();
                }

                await this.cartsService.RemoveProductFromShoppingCartByIdAsync(shoppingCartProductId);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
