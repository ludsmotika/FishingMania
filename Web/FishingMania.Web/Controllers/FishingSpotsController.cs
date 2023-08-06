namespace FishingMania.Web.Controllers
{
    using System;
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
        private readonly ICommentService commentService;

        public FishingSpotsController(IFishingSpotService fishingSpotService, ICommentService commentService)
        {
            this.fishingSpotService = fishingSpotService;
            this.commentService = commentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int page)
        {
            try
            {
                if (page < 1)
                {
                    page = 1;
                }

                const int itemsPerPage = 6;

                int maxPage = (int)Math.Ceiling((double)await this.fishingSpotService.GetCountAsync() / itemsPerPage);

                if (page > maxPage)
                {
                    page = maxPage;
                }

                List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsAsync(page, itemsPerPage);

                AllFishingSpotsPaginationViewModel fishingSpotsViewModel = new AllFishingSpotsPaginationViewModel()
                {
                    FishingSpots = fishingSpots,
                    ItemsPerPage = itemsPerPage,
                    PageNumber = page,
                    Count = await this.fishingSpotService.GetCountAsync(),
                };

                return this.View(fishingSpotsViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> FishingSpotsByType(FishingSpotType enumValue, int page)
        {
            try
            {
                if (page < 1)
                {
                    page = 1;
                }

                const int itemsPerPage = 3;

                int maxPage = (int)Math.Ceiling((double)await this.fishingSpotService.GetCountByTypeAsync(enumValue) / itemsPerPage);

                if (page > maxPage)
                {
                    page = maxPage;
                }

                List<FishingSpotViewModel> fishingSpots = await this.fishingSpotService.GetAllFishingSpotsByTypeAsync(enumValue, page, itemsPerPage);

                string spotTypeDescription = string.Empty;
                string spotTypeImageURL = string.Empty;
                switch (enumValue)
                {
                    case FishingSpotType.Reservoir:
                        spotTypeDescription = "A reservoir is an enlarged lake behind a dam. Such a dam may be either artificial, built to store fresh water or it may be a natural formation. Reservoirs can be created in a number of ways, including controlling a watercourse that drains an existing body of water, interrupting a watercourse to form an embayment within it, through excavation, or building any number of retaining walls or levees.";
                        spotTypeImageURL = "/img/other/reservoirPanorama.jpg";
                        this.ViewData["Title"] = "Reservoirs to fish on!";
                        break;
                    case FishingSpotType.Swamp:
                        spotTypeDescription = "A swamp is a forested wetland. Swamps are considered to be transition zones because both land and water play a role in creating this environment. Swamps vary in size and are located all around the world. The water of a swamp may be fresh water, brackish water, or seawater.";
                        spotTypeImageURL = "/img/other/swampPanorama.jpg";
                        this.ViewData["Title"] = "Swamps to fish on!";
                        break;
                    case FishingSpotType.Lake:
                        spotTypeDescription = "A lake is a naturally occurring, relatively large body of water localized in a basin surrounded by dry land.A lake generally has a slower-moving flow than the inflow or outflow stream(s) that serve to feed or drain it. Most lakes are freshwater and account for almost all the world's surface freshwater, but some are salt lakes with salinities even higher than that of seawater.";
                        spotTypeImageURL = "/img/other/lakePanorama.jpg";
                        this.ViewData["Title"] = "Lakes to fish on!";
                        break;
                    case FishingSpotType.River:
                        spotTypeDescription = "A river is a natural flowing watercourse, usually a freshwater stream, flowing on the surface or inside caves towards another waterbody at a lower elevation, such as an ocean, sea, bay, lake, wetland, or another river. In some cases, a river flows into the ground or becomes dry at the end of its course without reaching another body of water.";
                        spotTypeImageURL = "/img/other/riverPanorama.jpg";
                        this.ViewData["Title"] = "Rivers to fish on!";
                        break;
                    default:
                        break;
                }

                AllFishingSpotsByTypeViewModel viewModel = new AllFishingSpotsByTypeViewModel()
                {
                    FishingSpots = fishingSpots,
                    SpotTypeDescription = spotTypeDescription,
                    SpotTypeImageURL = spotTypeImageURL,
                    ItemsPerPage = itemsPerPage,
                    PageNumber = page,
                    FishingSpotType = enumValue,
                    Count = await this.fishingSpotService.GetCountByTypeAsync(enumValue),
                };

                return this.View($"ByType", viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                FishingSpotDetailsViewModel spotModel = await this.fishingSpotService.GetSpotForDetailsByIdAsync(id);

                if (spotModel == null)
                {
                    return this.RedirectToAction("All", "FishingSpots");
                }

                spotModel.Comments = await this.commentService.GetAllCommentsForThisEntityAsync(EntityWithCommentsType.Spot, id);

                return this.View(spotModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "FishingSpots");
            }
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
