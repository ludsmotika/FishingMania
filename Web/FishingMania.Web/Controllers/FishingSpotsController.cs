namespace FishingMania.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.FishingSpot;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class FishingSpotsController : Controller
    {
        private readonly IFishingSpotService fishingSpotService;

        public FishingSpotsController(IFishingSpotService fishingSpotService)
        {
            this.fishingSpotService = fishingSpotService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpots();
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Dams()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByType(FishingSpotType.Dam);
            return this.View(fishingSpots);
        }
    }
}
