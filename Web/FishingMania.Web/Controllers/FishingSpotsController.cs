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
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsAsync();
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Reservoirs()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(FishingSpotType.Reservoir);
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Lakes()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(FishingSpotType.Lake);
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Swamps()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(FishingSpotType.Swamp);
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Rivers()
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(FishingSpotType.River);
            return this.View(fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            FishingSpotDetailsViewModel spotModel = await this.fishingSpotService.GetSpotForDetailsById(id);
            return this.View(spotModel);
        }
    }
}
