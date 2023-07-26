namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Common;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.FishSpecies;
    using FishingMania.Web.ViewModels.Report;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static FishingMania.Web.Infrastructure.Common.ExceptionMessages;

    [Authorize]
    public class CatchesController : Controller
    {
        private readonly ICatchesService catchesService;
        private readonly IFishingSpotService fishingSpotService;
        private readonly ICommentService commentService;

        private readonly UserManager<ApplicationUser> userManager;

        public CatchesController(ICatchesService catchesService, IFishingSpotService fishingSpotService, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            this.catchesService = catchesService;
            this.fishingSpotService = fishingSpotService;
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllCatchesQueryViewModel queryModel)
        {
            try
            {
                AllCatchesFilteredAndPagedServiceModel serviceModel =
               await this.catchesService.GetAllCatchesAsync(queryModel);

                queryModel.Catches = serviceModel.Catches;
                queryModel.TotalCatches = serviceModel.TotalCatches;
                queryModel.Types = Enum.GetValues(typeof(FishType)).Cast<FishType>().ToList();

                return this.View(queryModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new CatchFormViewModel();

                model.FishingSpots = await this.fishingSpotService.AllForInputAsync();
                model.FishSpecies = new List<FishSpeciesDropdownViewModel>();

                return this.View(model);
            }
            catch (Exception)
            {
                throw;
            }
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
                if (!await this.fishingSpotService.FishingSpotHasFishSpeciesAsync(model.FishSpeciesId, model.FishingSpotId))
                {
                    model.FishingSpots = await this.fishingSpotService.AllForInputAsync();

                    this.ModelState.AddModelError("FishSpeciesId", "Invalid fish species for fishing spot!");
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                CatchDetailsViewModel catchModel = await this.catchesService.GetCatchByIdAsync(id);
                catchModel.Comments = await this.commentService.GetAllCommentsForThisEntityAsync(EntityWithCommentsType.Catch, id);

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
                return this.RedirectToAction("MyCatches", "ApplicationUser");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
