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
        private readonly IProductsService productsService;
        private readonly IProductCategoryService productCategoriesService;

        public ProductsController(IProductsService productsService, IProductCategoryService productCategoriesService)
        {
            this.productsService = productsService;
            this.productCategoriesService = productCategoriesService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllProductsQueryViewModel queryModel)
        {
            try
            {
                AllProductsFilteredAndPagedServiceModel serviceModel =
               await this.productsService.GetAllProductsAsync(queryModel);

                queryModel.Products = serviceModel.Products;
                queryModel.TotalProducts = serviceModel.TotalProducts;
                queryModel.Categories = await this.productCategoriesService.GetAllProductCategoriesAsync();

                return this.View(queryModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
