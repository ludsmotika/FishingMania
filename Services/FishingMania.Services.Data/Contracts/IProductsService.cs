namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Product;

    public interface IProductsService
    {
        Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsAsync(AllProductsQueryViewModel queryModel);

        Task<ProductDetailsViewModel> GetProductByIdAsync(int id);

        Task<bool> DoesProductExistByIdAsync(int id);
    }
}
