namespace FishingMania.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Report;
    using Microsoft.AspNetCore.Mvc;

    public class ReportsController : Controller
    {
        private readonly IReportsService reportsService;
        private readonly ICatchesService catchesService;

        public ReportsController(IReportsService reportsService, ICatchesService catchesService)
        {
            this.reportsService = reportsService;
            this.catchesService = catchesService;
        }

        [HttpGet]
        public IActionResult ReportPost(int catchId)
        {
            ReportInputViewModel viewModel = new ReportInputViewModel()
            {
                CatchId = catchId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReportPost(ReportInputViewModel viewModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            bool doesCatchExist = await this.catchesService.DoesCatchExist(viewModel.CatchId);

            if (doesCatchExist)
            {
                await this.reportsService.ReportCatch(viewModel.CatchId, currentUserId, viewModel.Complain);
            }

            return this.RedirectToAction("Details", "Catches", new { id = viewModel.CatchId });
        }
    }
}
