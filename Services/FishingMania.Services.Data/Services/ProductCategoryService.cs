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
    using FishingMania.Web.ViewModels.ProductCategory;
    using Microsoft.EntityFrameworkCore;

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IDeletableEntityRepository<ProductCategory> productsCategoriesRepository;

        public ProductCategoryService(IDeletableEntityRepository<ProductCategory> productsCategoriesRepository)
        {
            this.productsCategoriesRepository = productsCategoriesRepository;
        }

        public async Task<List<ProductCategoryViewModel>> GetAllProductCategoriesAsync()
        {
            return await this.productsCategoriesRepository.All().To<ProductCategoryViewModel>().ToListAsync();
        }
    }
}
