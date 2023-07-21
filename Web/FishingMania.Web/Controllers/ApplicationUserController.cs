﻿namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Cart;
    using FishingMania.Web.ViewModels.Catch;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICatchesService catchesService;
        private readonly ICartsService cartsService;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, ICatchesService catchesService, ICartsService cartsService)
        {
            this.userManager = userManager;
            this.catchesService = catchesService;
            this.cartsService = cartsService;
        }

        public async Task<IActionResult> MyCart()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await this.cartsService.DoesUserHasCartAsync(currentUserId))
            {
                await this.cartsService.CreateCartForUserByIdAsync(currentUserId);
            }

            ShoppingCartViewModel cartModel = await this.cartsService.GetCartByUserIdAsync(currentUserId);

            return this.View(cartModel);
        }

        //TODO: create custom page for approving account in this controller

        public async Task<IActionResult> MyCatches()
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
    }
}