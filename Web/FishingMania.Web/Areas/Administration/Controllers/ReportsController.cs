namespace FishingMania.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ReportsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Report> reportsRepository;

        public ReportsController(IDeletableEntityRepository<Report> reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        // GET: Administration/Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.reportsRepository.AllWithDeleted().Include(r => r.ApplicationUser).Include(r => r.Catch).OrderByDescending(r => r.CreatedOn);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Reports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || this.reportsRepository == null)
            {
                return this.NotFound();
            }

            var report = await this.reportsRepository.AllWithDeleted()
                .Include(r => r.ApplicationUser)
                .Include(r => r.Catch)
                .ThenInclude(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return this.NotFound();
            }

            return this.View(report);
        }

        // GET: Administration/Reports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || this.reportsRepository == null)
            {
                return this.NotFound();
            }

            var report = await this.reportsRepository.AllWithDeleted()
                .Include(r => r.ApplicationUser)
                .Include(r => r.Catch)
                .ThenInclude(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return this.NotFound();
            }

            return this.View(report);
        }

        // POST: Administration/Reports/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (this.reportsRepository == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Reports'  is null.");
            }
            var report = await this.reportsRepository.All().FirstOrDefaultAsync(r => r.Id == id);
            if (report != null)
            {
                this.reportsRepository.Delete(report);
            }

            await this.reportsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ReportExists(Guid id)
        {
            return this.reportsRepository.All().Any(e => e.Id == id);
        }
    }
}
