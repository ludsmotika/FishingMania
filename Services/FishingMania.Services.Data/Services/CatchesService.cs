namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using Microsoft.EntityFrameworkCore;

    public class CatchesService : ICatchesService
    {
        private readonly IDeletableEntityRepository<Catch> catchesRepository;
        private readonly IImageService imageService;

        public CatchesService(IDeletableEntityRepository<Catch> catchesRepository, IImageService imageService)
        {
            this.catchesRepository = catchesRepository;
            this.imageService = imageService;
        }

        public async Task CreateAsync(CatchFormViewModel model, string userId)
        {
            // We don't need any validation on this stage so we just make the Catch object and add it to the repo
            var image = await this.imageService.AddByFile(model.ImageFile, model.ImageFile.FileName);

            Catch catchToAdd = new Catch()
            {
                Description = model.Description,
                FishWeight = model.Weight,
                Image = image,
                ApplicationUserId = userId,
                FishingSpotId = model.FishingSpotId,
                FishSpeciesId = model.FishSpeciesId,
            };

            await this.catchesRepository.AddAsync(catchToAdd);
            await this.catchesRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Catch catchToDel = await this.catchesRepository.All().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (catchToDel == null)
            {
                throw new ArgumentException();
            }

            this.catchesRepository.Delete(catchToDel);
            await this.catchesRepository.SaveChangesAsync();

        }

        public async Task<List<CatchViewModel>> GetAllCatchesAsync()
        {
            return await this.catchesRepository.AllAsNoTracking().To<CatchViewModel>().ToListAsync();
        }

        public async Task<CatchDetailsViewModel> GetCatchByIdAsync(int id)
        {
            return await this.catchesRepository.All().Where(x => x.Id == id).Include(p => p.FishingSpot.Image).Include(p => p.FishSpecies.Image).To<CatchDetailsViewModel>().FirstOrDefaultAsync();
        }

        public async Task<List<CatchViewModel>> GetCatchesByUserIdAsync(string id)
        {
            return await this.catchesRepository.All().Where(c => c.ApplicationUserId == id).To<CatchViewModel>().ToListAsync();
        }
    }
}
