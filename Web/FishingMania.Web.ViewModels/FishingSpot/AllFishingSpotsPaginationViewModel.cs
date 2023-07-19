namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;

    using FishingMania.Web.ViewModels.Base;

    public class AllFishingSpotsPaginationViewModel : PaginationViewModel
    {
        public IEnumerable<FishingSpotViewModel> FishingSpots { get; set; } = new HashSet<FishingSpotViewModel>();
    }
}
