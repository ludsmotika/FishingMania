namespace FishingMania.Web.ViewModels.FishSpecies
{
    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class FishSpeciesDropdownViewModel : IMapFrom<FishSpecies>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
