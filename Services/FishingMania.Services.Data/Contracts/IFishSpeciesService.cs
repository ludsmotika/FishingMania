namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.FishSpecies;

    public interface IFishSpeciesService
    {
        Task<List<FishSpeciesDropdownViewModel>> AllForInputAsync();
    }
}
