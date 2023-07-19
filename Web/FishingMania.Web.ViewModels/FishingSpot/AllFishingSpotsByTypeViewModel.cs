namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.Base;

    public class AllFishingSpotsByTypeViewModel : PaginationViewModel
    {
        public string SpotTypeDescription { get; set; } = null!;

        public string SpotTypeImageURL { get; set; } = null!;

        public FishingSpotType FishingSpotType { get; set; }

        public List<FishingSpotViewModel> FishingSpots { get; set; } = new List<FishingSpotViewModel>();

    }
}
