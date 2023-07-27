using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishingMania.Data;
using FishingMania.Data.Models;
using FishingMania.Data.Common.Repositories;
using FishingMania.Web.ViewModels.FishingSpot;
using FishingMania.Services.Data.Contracts;

namespace FishingMania.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class FishingSpotsController : Controller
    {
        private readonly IDeletableEntityRepository<FishingSpot> fishingSpotsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IImageService imageService;

        public FishingSpotsController(ApplicationDbContext context, IDeletableEntityRepository<FishingSpot> fishingSpotsRepository, IDeletableEntityRepository<Image> imagesRepository, IImageService imageService)
        {
            this.fishingSpotsRepository = fishingSpotsRepository;
            this.imagesRepository = imagesRepository;
            this.imageService = imageService;
        }

        // GET: Administration/FishingSpots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.fishingSpotsRepository.AllWithDeleted().Include(f => f.Image);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/FishingSpots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.fishingSpotsRepository == null)
            {
                return this.NotFound();
            }

            var fishingSpot = await this.fishingSpotsRepository.AllWithDeleted()
                .Include(f => f.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishingSpot == null)
            {
                return this.NotFound();
            }

            return this.View(fishingSpot);
        }

        // GET: Administration/FishingSpots/Create
        public IActionResult Create()
        {
            FishingSpotFormViewModel fishingSpot = new FishingSpotFormViewModel();
            return this.View(fishingSpot);
        }

        // POST: Administration/FishingSpots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FishingSpotFormViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                Image image = await this.imageService.AddByFile(viewModel.ImageFile, viewModel.ImageFile.FileName);

                FishingSpot fishingSpot = new FishingSpot
                {
                    Latitude = viewModel.Latitude,
                    Longitude = viewModel.Longitude,
                    Description = viewModel.Description,
                    Name = viewModel.Name,
                    CreatedOn = DateTime.Now,
                    FishingSpotType = viewModel.FishingSpotType,
                    Image = image,
                };

                await this.fishingSpotsRepository.AddAsync(fishingSpot);
                await this.fishingSpotsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(viewModel);
        }

        // GET: Administration/FishingSpots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.fishingSpotsRepository == null)
            {
                return this.NotFound();
            }

            var fishingSpot = await this.fishingSpotsRepository.AllWithDeleted().FirstOrDefaultAsync(s => s.Id == id);
            if (fishingSpot == null)
            {
                return this.NotFound();
            }

            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.AllWithDeleted(), "Id", "URL", fishingSpot.ImageId);
            return this.View(fishingSpot);
        }

        // POST: Administration/FishingSpots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Latitude,Longitude,FishingSpotType,ImageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] FishingSpot fishingSpot)
        {
            if (id != fishingSpot.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.fishingSpotsRepository.Update(fishingSpot);
                    await this.fishingSpotsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.FishingSpotExists(fishingSpot.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.AllWithDeleted(), "Id", "URL", fishingSpot.ImageId);
            return this.View(fishingSpot);
        }

        // GET: Administration/FishingSpots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.fishingSpotsRepository == null)
            {
                return this.NotFound();
            }

            var fishingSpot = await this.fishingSpotsRepository.All()
                .Include(f => f.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishingSpot == null)
            {
                return this.NotFound();
            }

            return this.View(fishingSpot);
        }

        // POST: Administration/FishingSpots/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.fishingSpotsRepository == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.FishingSpots'  is null.");
            }
            var fishingSpot = await this.fishingSpotsRepository.All().FirstOrDefaultAsync(s => s.Id == id);
            if (fishingSpot != null)
            {
                this.fishingSpotsRepository.Delete(fishingSpot);
            }

            await this.fishingSpotsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool FishingSpotExists(int id)
        {
            return this.fishingSpotsRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
