namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Common;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Catch;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static FishingMania.Web.Infrastructure.Common.ExceptionMessages;

    [Authorize]
    public class CatchesController : Controller
    {
        private readonly ICatchesService catchesService;
        private readonly IFishingSpotService fishingSpotService;
        private readonly IFishSpeciesService fishSpeciesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CatchesController(ICatchesService catchesService, IFishingSpotService fishingSpotService, IFishSpeciesService fishSpeciesService, UserManager<ApplicationUser> userManager)
        {
            this.catchesService = catchesService;
            this.fishingSpotService = fishingSpotService;
            this.fishSpeciesService = fishSpeciesService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            List<CatchViewModel> catches = await this.catchesService.GetAllCatches();
            return this.View(catches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CatchFormViewModel();
            model.FishingSpots = await this.fishingSpotService.AllForInputAsync();
            model.FishSpecies = await this.fishSpeciesService.AllForInputAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatchFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.FishingSpots = await this.fishingSpotService.AllForInputAsync();
                model.FishSpecies = await this.fishSpeciesService.AllForInputAsync();
                return this.View(model);
            }

            try
            {
                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);
                await this.catchesService.CreateAsync(model, applicationUser.Id);

                this.TempData[GlobalConstants.SuccessMessage] = SuccessfullyAddedMessage;
                return this.RedirectToAction("All", "Catches");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                model.FishingSpots = await this.fishingSpotService.AllForInputAsync();
                model.FishSpecies = await this.fishSpeciesService.AllForInputAsync();
                return this.View(model);
            }
        }
    }
}
