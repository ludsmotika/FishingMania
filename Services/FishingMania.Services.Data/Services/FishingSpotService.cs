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

    public class FishingSpotService : IFishingSpotService
    {
        private readonly IDeletableEntityRepository<FishingSpot> fishingSpotsRepository;
        private readonly IFishSpeciesService fishSpeciesService;

        public FishingSpotService(IDeletableEntityRepository<FishingSpot> fishingSpotsRepository, IFishSpeciesService fishSpeciesService)
        {
            this.fishingSpotsRepository = fishingSpotsRepository;
            this.fishSpeciesService = fishSpeciesService;
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

        public async Task<FishingSpotDetailsViewModel> GetSpotForDetailsById(int id)
        {
            FishingSpot fishingSpot = await this.fishingSpotsRepository
                                                .AllAsNoTracking().Where(fs => fs.Id == id)
                                                .Include(fs => fs.Image)
                                                .Include(fs => fs.FishSpeciesFishingSpots)
                                                .ThenInclude(fsfs => fsfs.FishSpecies)
                                                .ThenInclude(fsfs => fsfs.Image)
                                                .IgnoreQueryFilters()
                                                .FirstOrDefaultAsync();

            FishingSpotDetailsViewModel viewModel = new FishingSpotDetailsViewModel()
            {
                Id = fishingSpot.Id,
                Name = fishingSpot.Name,
                Description = fishingSpot.Description,
                Latitude = fishingSpot.Latitude,
                Longitude = fishingSpot.Longitude,
                ImageId = fishingSpot.ImageId,
                Image = fishingSpot.Image,
                FishSpecies = fishingSpot.FishSpeciesFishingSpots
                                         .Select(fs => new FishSpeciesViewModel()
                                         {
                                             Image = fs.FishSpecies.Image,
                                             Name = fs.FishSpecies.Name,
                                         }).ToList(),
            };

            return viewModel;
        }
    }
}
