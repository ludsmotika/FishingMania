namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;

    public class FishingSpotByTypeViewModel
    {
        public string SpotTypeDescription { get; set; } = null!;

        public string SpotTypeImageURL { get; set; } = null!;

        public List<FishingSpotViewModel> FishingSpots { get; set; } = new List<FishingSpotViewModel>();

    }
}
