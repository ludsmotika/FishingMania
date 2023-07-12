namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualBasic;

    public class FishingSpotService : IFishingSpotService
    {
        private readonly IDeletableEntityRepository<FishingSpot> fishingSpotsRepository;

        public FishingSpotService(IDeletableEntityRepository<FishingSpot> fishingSpotsRepository)
        {
            this.fishingSpotsRepository = fishingSpotsRepository;
        }

        public async Task<List<FishingSpotDropdownViewModel>> AllForInputAsync()
        {
            List<FishingSpotDropdownViewModel> fishingSpots =
                await this.fishingSpotsRepository.All()
                                                 .Select(fs => new FishingSpotDropdownViewModel() { Id = fs.Id, Name = fs.Name })
                                                 .ToListAsync();

            return fishingSpots;
        }

        public async Task<List<FishingSpotViewModel>> GetAllFishingSpotsAsync()
        {
            return await this.fishingSpotsRepository.AllAsNoTracking().To<FishingSpotViewModel>().ToListAsync();
        }

        public async Task<List<FishingSpotViewModel>> GetAllFishingSpotsByTypeAsync(FishingSpotType type)
        {
            return await this.fishingSpotsRepository.AllAsNoTracking().Where(fs => fs.FishingSpotType == type).To<FishingSpotViewModel>().ToListAsync();
        }

        public async Task<List<FishSpeciesDropdownViewModel>> GetFishSpeciesForSpotByIdAsync(int id)
        {
            return await this.fishingSpotsRepository.All().Where(fs => fs.Id == id).SelectMany(fs => fs.FishSpecies).To<FishSpeciesDropdownViewModel>().ToListAsync();
        }

        public async Task<FishingSpotDetailsViewModel> GetSpotForDetailsById(int id)
        {
            FishingSpotDetailsViewModel fishingSpot = await this.fishingSpotsRepository
                                                .AllAsNoTracking().Where(fs => fs.Id == id)
                                                .Include(fs => fs.Image)
                                                .Include(fs => fs.FishSpecies)
                                                .ThenInclude(fsfs => fsfs.Image)
                                                .To<FishingSpotDetailsViewModel>()
                                                .FirstOrDefaultAsync();

            return fishingSpot;
        }

        public async Task<bool> FishingSpotHasFishSpecies(int fishSpeciesId, int fishingSpotId)
        {
            return await this.fishingSpotsRepository.All().Where(fs => fs.Id == fishingSpotId).SelectMany(fs => fs.FishSpecies).AnyAsync(x => x.Id == fishSpeciesId);
        }
    }
}
