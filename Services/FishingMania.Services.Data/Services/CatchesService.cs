namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public CatchesService(IDeletableEntityRepository<Catch> catchesRepository)
        {
            this.catchesRepository = catchesRepository;
        }

        public async Task<List<CatchViewModel>> GetAllCatches()
        {
            return await this.catchesRepository.AllAsNoTracking().To<CatchViewModel>().ToListAsync();
        }
    }
}
