namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;

    public interface IFishingSpotService
    {
        Task<int> GetCountAsync();

        Task<int> GetCountByTypeAsync(FishingSpotType type);

        Task<List<FishingSpotViewModel>> GetAllFishingSpotsAsync(int page, int itemsPerPage);

        Task<List<FishingSpotViewModel>> GetAllFishingSpotsByTypeAsync(FishingSpotType type, int page, int itemsPerPage);

        Task<List<FishingSpotDropdownViewModel>> AllForInputAsync();

        Task<FishingSpotDetailsViewModel> GetSpotForDetailsByIdAsync(int id);

        Task<List<FishSpeciesDropdownViewModel>> GetFishSpeciesForSpotByIdAsync(int id);

        Task<bool> FishingSpotHasFishSpeciesAsync(int fishSpeciesId, int fishingSpotId);
    }
}
