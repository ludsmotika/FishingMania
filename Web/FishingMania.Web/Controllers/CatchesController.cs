namespace FishingMania.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.FishingSpot;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CatchesController : Controller
    {
        private readonly ICatchesService catchesService;

        public CatchesController(ICatchesService catchesService)
        {
            this.catchesService = catchesService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            List<CatchViewModel> catches = await this.catchesService.GetAllCatches();
            return this.View(catches);
        }
    }
}
