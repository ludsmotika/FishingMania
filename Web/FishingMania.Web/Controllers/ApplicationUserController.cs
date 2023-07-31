namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Messaging;
    using FishingMania.Web.ViewModels.Cart;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.ContactUs;
    using FishingMania.Web.ViewModels.Order;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICatchesService catchesService;
        private readonly ICartsService cartsService;
        private readonly IOrdersService ordersService;
        private readonly IEmailSender emailSender;


        public ApplicationUserController(UserManager<ApplicationUser> userManager, ICatchesService catchesService, ICartsService cartsService, IOrdersService ordersService, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.catchesService = catchesService;
            this.cartsService = cartsService;
            this.ordersService = ordersService;
            this.emailSender = emailSender;
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

        public async Task<IActionResult> MyOrders()
        {
            try
            {
                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

                if (applicationUser == null)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                List<OrderViewModel> ordersOfUser = await this.ordersService.GetOrdersForUserByIsAsync(applicationUser.Id);
                return this.View(ordersOfUser);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> OrderDetails(string id)
        {
            try
            {
                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

                if (applicationUser == null)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                List<OrderViewModel> order = await this.ordersService.GetOrdersForUserByIsAsync(applicationUser.Id);
                return this.View(order);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            ContactUsFormViewModel viewModel = new ContactUsFormViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUsFormViewModel viewModel)
        {
            try
            {
                ApplicationUser applicationUser = await this.userManager.GetUserAsync(this.User);

                if (applicationUser == null || applicationUser.EmailConfirmed == false)
                {
                    return this.RedirectToAction("Store", "Home");
                }

                viewModel.ApplicationUser = applicationUser;
                viewModel.ApplicationUserId = applicationUser.Id;

                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction("Store", "Home");
                }

                StringBuilder html = new StringBuilder();
                html.AppendLine($"<h2>UserName: {viewModel.ApplicationUser.UserName}</h2>");
                html.AppendLine($"<h2>User Email: {viewModel.ApplicationUser.Email}</h2>");
                html.AppendLine($"<h2>Complain: {viewModel.Content}</h2>");

                if (viewModel.Content != null)
                {
                    await this.emailSender.SendEmailAsync("fishingmaniabg@abv.bg", "Daniel Stefanov", "fishingmaniabg@abv.bg", viewModel.Topic.ToString(), html.ToString());
                }

                return this.RedirectToAction("Store", "Home");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
