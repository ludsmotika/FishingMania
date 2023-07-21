namespace FishingMania.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;

        public CartsController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("AddProductToCartAsync")]
        [ActionName("AddProductToCartAsync")]
        public async Task AddProductToCartAsync(ProductAddToCartViewModel model)
        {
            try
            {
                if (!await this.cartsService.DoesUserHasCartAsync(model.ApplicationUserId))
                {
                    await this.cartsService.CreateCartForUserByIdAsync(model.ApplicationUserId);
                }

                await this.cartsService.AddProductToCartByIds(model.ProductId, model.ApplicationUserId, model.Amount);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
