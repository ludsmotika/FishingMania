namespace FishingMania.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsServise;

        public ProductsController(IProductsService productsServise)
        {
            this.productsServise = productsServise;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllProductsQueryViewModel queryModel)
        {
            try
            {
                AllProductsFilteredAndPagedServiceModel serviceModel =
               await this.productsServise.GetAllProductsAsync(queryModel);

                queryModel.Products = serviceModel.Products;
                queryModel.TotalProducts = serviceModel.TotalProducts;
                //queryModel.Category = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToList();

                return this.View(queryModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
