namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.ProductCategory;

    public interface IProductCategoryService
    {
        Task<List<ProductCategoryViewModel>> GetAllProductCategoriesAsync();
    }
}
