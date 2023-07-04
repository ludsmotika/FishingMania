namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.EntityFrameworkCore;

    public class FishSpeciesService : IFishSpeciesService
    {
        private readonly IDeletableEntityRepository<FishSpecies> fishSpeciesRepository;

        public FishSpeciesService(IDeletableEntityRepository<FishSpecies> fishSpeciesRepository)
        {
            this.fishSpeciesRepository = fishSpeciesRepository;
        }

        public async Task<List<FishSpeciesDropdownViewModel>> AllForInputAsync()
        {
            List<FishSpeciesDropdownViewModel> fishSpeciesDropdownViewModels =
                await this.fishSpeciesRepository.All()
                                                .Select(fs => new FishSpeciesDropdownViewModel() { Id = fs.Id, Name = fs.Name })
                                                .ToListAsync();

            return fishSpeciesDropdownViewModels;
        }
    }
}
