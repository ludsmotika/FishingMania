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
    using FishingMania.Web.ViewModels.FishingSpot;
    using Microsoft.EntityFrameworkCore;

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
            return await this.fishingSpotsRepository.AllAsNoTracking().Where(x => x.FishingSpotType == type).To<FishingSpotViewModel>().ToListAsync();
        }
    }
}
