namespace FishingMania.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.FishingSpot;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class FishingSpotsController : AdministrationController
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
            return this.View(await applicationDbContext.ToListAsync());
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

            var fishingSpot = await this.fishingSpotsRepository.AllWithDeleted().Where(s => s.Id == id).To<FishingSpotEditFormViewModel>().FirstOrDefaultAsync();
            if (fishingSpot == null)
            {
                return this.NotFound();
            }

            List<SelectListItem> fishingSpotTypes = new List<SelectListItem>();

            var types = Enum.GetValues<FishingSpotType>().ToList();

            foreach (var type in types)
            {
                fishingSpotTypes.Add(new SelectListItem()
                {
                    Text = type.ToString(),
                    Value = type.ToString(),
                });
            }

            this.ViewBag.FishingSpotTypes = fishingSpotTypes;

            return this.View(fishingSpot);
        }

        // POST: Administration/FishingSpots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FishingSpotEditFormViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    FishingSpot fishingSpotToEdit = await this.fishingSpotsRepository.AllWithDeleted().FirstOrDefaultAsync(s => s.Id == id);

                    fishingSpotToEdit.Name = viewModel.Name;
                    fishingSpotToEdit.Description = viewModel.Description;
                    fishingSpotToEdit.Longitude = viewModel.Longitude;
                    fishingSpotToEdit.Latitude = viewModel.Latitude;
                    fishingSpotToEdit.FishingSpotType = viewModel.FishingSpotType;
                    fishingSpotToEdit.IsDeleted = viewModel.IsDeleted;

                    if (viewModel.IsDeleted == true && viewModel.DeletedOn == null)
                    {
                        fishingSpotToEdit.DeletedOn = DateTime.Now;
                    }
                    else if (viewModel.IsDeleted == true && viewModel.DeletedOn != null)
                    {
                        fishingSpotToEdit.DeletedOn = viewModel.DeletedOn;
                    }
                    else if (viewModel.IsDeleted == false && viewModel.DeletedOn != null)
                    {
                        fishingSpotToEdit.DeletedOn = null;
                    }

                    if (viewModel.ImageFile != null)
                    {
                        Image newImage = await this.imageService.AddByFile(viewModel.ImageFile, viewModel.ImageFile.FileName);
                        fishingSpotToEdit.Image = newImage;
                        fishingSpotToEdit.ImageId = newImage.Id;
                    }

                    this.fishingSpotsRepository.Update(fishingSpotToEdit);
                    await this.fishingSpotsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(viewModel);
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
