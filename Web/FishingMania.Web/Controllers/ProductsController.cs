using Microsoft.AspNetCore.Mvc;

namespace FishingMania.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
