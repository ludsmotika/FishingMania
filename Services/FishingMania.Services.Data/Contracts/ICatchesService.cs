namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Web.ViewModels.Catch;

    public interface ICatchesService
    {
        Task<AllCatchesFilteredAndPagedServiceModel> GetAllCatchesAsync(AllCatchesQueryViewModel queryModel);

        Task<List<CatchViewModel>> GetCatchesByUserIdAsync(string id);

        Task CreateAsync(CatchFormViewModel model, string userId);

        Task<CatchDetailsViewModel> GetCatchByIdAsync(int id);

        Task DeleteByIdAsync(int id);

        Task<bool> DoesCatchExist(int id);
    }
}
