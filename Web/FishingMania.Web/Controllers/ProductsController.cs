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
        private readonly ICommentService commentsService;
        private readonly IProductCategoryService productCategoriesService;

        public ProductsController(IProductsService productsService, IProductCategoryService productCategoriesService, ICommentService commentsService)
        {
            this.productsService = productsService;
            this.productCategoriesService = productCategoriesService;
            this.commentsService = commentsService;
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

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ProductDetailsViewModel productModel = await this.productsService.GetProductByIdAsync(id);

                if (productModel == null)
                {
                    return this.RedirectToAction("All", "Products");
                }

                productModel.Comments = await this.commentsService.GetAllCommentsForThisEntityAsync(EntityWithCommentsType.Product, id);

                return this.View(productModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
