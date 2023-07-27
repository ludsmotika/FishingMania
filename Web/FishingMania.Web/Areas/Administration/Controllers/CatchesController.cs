namespace FishingMania.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    [Area("Administration")]
    public class CatchesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Catch> catchesRepository;

        public CatchesController(IDeletableEntityRepository<Catch> catchesRepository)
        {
            this.catchesRepository = catchesRepository;
        }

        // GET: Administration/Catches
        public async Task<IActionResult> Index(string searchQuery = "")
        {
            IIncludableQueryable<Catch, Image> catches;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                string wildCard = $"%{searchQuery.ToLower()}%";
                catches = this.catchesRepository.AllWithDeleted()
                    .Where(c => EF.Functions.Like(c.Description, wildCard) || EF.Functions.Like(c.FishSpecies.Name, wildCard) || EF.Functions.Like(c.FishingSpot.Name, wildCard) || EF.Functions.Like(c.ApplicationUser.UserName, wildCard))
                    .Include(c => c.ApplicationUser)
                    .Include(c => c.FishSpecies)
                    .Include(c => c.FishingSpot)
                    .Include(c => c.Image);
            }
            else
            {
                catches = this.catchesRepository.AllWithDeleted().Include(c => c.ApplicationUser).Include(c => c.FishSpecies).Include(c => c.FishingSpot).Include(c => c.Image);
            }

            return this.View(await catches.OrderByDescending(c => c.CreatedOn).ToListAsync());
        }

        // GET: Administration/Catches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.catchesRepository == null)
            {
                return this.NotFound();
            }

            var @catch = await this.catchesRepository.AllWithDeleted()
                .Include(c => c.ApplicationUser)
                .Include(c => c.FishSpecies)
                .Include(c => c.FishingSpot)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@catch == null)
            {
                return this.NotFound();
            }

            return this.View(@catch);
        }

        // GET: Administration/Catches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.catchesRepository == null)
            {
                return this.NotFound();
            }

            var @catch = await this.catchesRepository.All()
                .Include(c => c.ApplicationUser)
                .Include(c => c.FishSpecies)
                .Include(c => c.FishingSpot)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@catch == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(@catch);
        }

        // POST: Administration/Catches/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.catchesRepository == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Catches'  is null.");
            }

            var @catch = await this.catchesRepository.All().FirstOrDefaultAsync(c => c.Id == id);
            if (@catch != null)
            {
                this.catchesRepository.Delete(@catch);
            }

            await this.catchesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CatchExists(int id)
        {
            return this.catchesRepository.All().Any(e => e.Id == id);
        }
    }
}
