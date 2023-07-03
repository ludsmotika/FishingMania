namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.FishingSpot;

    public interface IFishingSpotService
    {
        Task<List<FishingSpotViewModel>> GetAllFishingSpots();

        Task<List<FishingSpotViewModel>> GetAllFishingSpotsByType(FishingSpotType type);
    }
}
