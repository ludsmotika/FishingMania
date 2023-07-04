namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Catch;

    public interface ICatchesService
    {
        Task<List<CatchViewModel>> GetAllCatchesAsync();

        Task<List<CatchViewModel>> GetCatchesByUserIdAsync(string id);

        Task CreateAsync(CatchFormViewModel model, string userId);

        Task<CatchDetailsViewModel> GetCatchByIdAsync(int id);
    }
}
