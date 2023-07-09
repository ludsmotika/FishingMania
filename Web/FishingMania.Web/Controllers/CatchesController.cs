namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Common;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static FishingMania.Web.Infrastructure.Common.ExceptionMessages;

    [Authorize]
    public class CatchesController : Controller
    {
        private readonly ICatchesService catchesService;
        private readonly IFishingSpotService fishingSpotService;
        private readonly UserManager<ApplicationUser> userManager;

        public CatchesController(ICatchesService catchesService, IFishingSpotService fishingSpotService, UserManager<ApplicationUser> userManager)
        {
            this.catchesService = catchesService;
            this.fishingSpotService = fishingSpotService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            List<CatchViewModel> catches = await this.catchesService.GetAllCatchesAsync();
            return this.View(catches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CatchFormViewModel();

            model.FishingSpots = await this.fishingSpotService.AllForInputAsync();
            model.FishSpecies = new List<FishSpeciesDropdownViewModel>();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatchFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.FishingSpots = await this.fishingSpotService.AllForInputAsync();

                return this.View(model);
            }

            try
            {
                if (!await this.fishingSpotService.FishingSpotHasFishSpecies(model.FishSpeciesId, model.FishingSpotId))
                {
                    model.FishingSpots = await this.fishingSpotService.AllForInputAsync();

                    return this.View(model);
                }

                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);
                await this.catchesService.CreateAsync(model, applicationUser.Id);

                this.TempData[GlobalConstants.SuccessMessage] = SuccessfullyAddedMessage;
                return this.RedirectToAction("All", "Catches");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                model.FishingSpots = await this.fishingSpotService.AllForInputAsync();

                return this.View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

                if (applicationUser == null)
                {
                    return this.RedirectToAction("All", "Catches");
                }

                List<CatchViewModel> catchesOfUser = await this.catchesService.GetCatchesByUserIdAsync(applicationUser.Id);
                return this.View(catchesOfUser);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.RedirectToAction("All", "Catches");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            // Get the post from the service, bind it to view model and pass it to the view
            try
            {
                CatchDetailsViewModel catchModel = await this.catchesService.GetCatchByIdAsync(id);

                if (catchModel == null)
                {
                    return this.RedirectToAction("All", "Catches");
                }

                return this.View(catchModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.catchesService.DeleteByIdAsync(id);
                return this.RedirectToAction("Mine", "Catches");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
