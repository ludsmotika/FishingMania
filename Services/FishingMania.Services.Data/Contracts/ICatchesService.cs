namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Catch;

    public interface ICatchesService
    {
        Task<List<CatchViewModel>> GetAllCatches();
    }
}
