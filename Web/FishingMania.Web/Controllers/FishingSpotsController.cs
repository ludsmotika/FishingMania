namespace FishingMania.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;
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
        public async Task<IActionResult> FishingSpotsByType(FishingSpotType enumValue)
        {
            List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(enumValue);
            return this.View($"{enumValue}s", fishingSpots);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            FishingSpotDetailsViewModel spotModel = await this.fishingSpotService.GetSpotForDetailsByIdAsync(id);
            return this.View(spotModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFishingSpotSpeciesOptions(int fishingSpotId)
        {
            List<FishSpeciesDropdownViewModel> childOptions = await this.fishingSpotService.GetFishSpeciesForSpotByIdAsync(fishingSpotId);

            // Convert child options to a format suitable for JSON serialization
            var jsonData = childOptions.Select(option => new
            {
                value = option.Id,
                text = option.Name,
            });

            return this.Json(jsonData);
        }

    }
}
