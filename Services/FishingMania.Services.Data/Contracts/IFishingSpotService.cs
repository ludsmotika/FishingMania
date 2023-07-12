namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;

    public interface IFishingSpotService
    {
        Task<List<FishingSpotViewModel>> GetAllFishingSpotsAsync();

        Task<List<FishingSpotViewModel>> GetAllFishingSpotsByTypeAsync(FishingSpotType type);

        Task<List<FishingSpotDropdownViewModel>> AllForInputAsync();

        Task<FishingSpotDetailsViewModel> GetSpotForDetailsByIdAsync(int id);

        Task<List<FishSpeciesDropdownViewModel>> GetFishSpeciesForSpotByIdAsync(int id);

        Task<bool> FishingSpotHasFishSpeciesAsync(int fishSpeciesId, int fishingSpotId);
    }
}
