namespace FishingMania.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class OrdersController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersController(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        // GET: Administration/Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.ordersRepository.AllWithDeleted().Include(o => o.ApplicationUser);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || this.ordersRepository == null)
            {
                return this.NotFound();
            }

            var order = await this.ordersRepository.AllWithDeleted()
                .Include(o => o.ApplicationUser)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        // GET: Administration/Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || this.ordersRepository == null)
            {
                return this.NotFound();
            }

            var order = await this.ordersRepository.All()
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        // POST: Administration/Orders/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (this.ordersRepository == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await this.ordersRepository.All().FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                this.ordersRepository.Delete(order);
            }

            await this.ordersRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool OrderExists(string id)
        {
            return this.ordersRepository.All().Any(e => e.Id == id);
        }
    }
}
