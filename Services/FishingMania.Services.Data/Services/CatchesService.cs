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
    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Catch.Enums;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
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

        public Task<bool> DoesCatchExist(int id)
        {
            return this.catchesRepository.All().Where(c => c.Id == id).AnyAsync();
        }

        public async Task<AllCatchesFilteredAndPagedServiceModel> GetAllCatchesAsync(AllCatchesQueryViewModel queryModel)
        {
            IQueryable<Catch> catchesQuery = this.catchesRepository.All().AsQueryable();

            if (queryModel.CurrentPage <= 0)
            {
                queryModel.CurrentPage = 1;
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Type.ToString()))
            {
                catchesQuery = catchesQuery
                    .Where(c => c.FishSpecies.Type == queryModel.Type);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                catchesQuery = catchesQuery
                    .Where(c => EF.Functions.Like(c.Description, wildCard) ||
                                EF.Functions.Like(c.FishSpecies.Name, wildCard) ||
                                EF.Functions.Like(c.FishingSpot.Name, wildCard));
            }

            catchesQuery = queryModel.CatchesSorting switch
            {
                CatchesSorting.Newest => catchesQuery
                    .OrderByDescending(c => c.CreatedOn),
                CatchesSorting.Oldest => catchesQuery
                    .OrderBy(c => c.CreatedOn),
                CatchesSorting.FishWeightAscending => catchesQuery
                    .OrderBy(c => c.FishWeight),
                CatchesSorting.FishWeightDescending => catchesQuery
                    .OrderByDescending(c => c.FishWeight),
                _ => catchesQuery.OrderBy(c => c.CreatedOn).ThenByDescending(c => c.CreatedOn),
            };

            IEnumerable<CatchViewModel> allCatches = await catchesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.CatchesPerPage)
                .Take(queryModel.CatchesPerPage)
                .To<CatchViewModel>()
                .ToArrayAsync();

            if (catchesQuery.Count() != 0)
            {
                int maxPage = (int)Math.Ceiling((double)catchesQuery.Count() / queryModel.CatchesPerPage);

                if (queryModel.CurrentPage > maxPage && maxPage != 0)
                {
                    throw new ArgumentException();
                }
            }


            int totalCatches = catchesQuery.Count();

            return new AllCatchesFilteredAndPagedServiceModel()
            {
                TotalCatches = totalCatches,
                Catches = allCatches,
            };

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
