namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddFishSpeciesToSpotViewModel
    {
        public int SpotId { get; set; }

        public List<SelectListItem> FishSpecies { get; set; }

        public List<int> AllreadyAddedFishSpeciesIds { get; set; }
    }
}
