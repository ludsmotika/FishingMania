namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Data.ServiceModels;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Product;
    using FishingMania.Web.ViewModels.Product.Enums;
    using Microsoft.EntityFrameworkCore;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;


        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> GetAllProductsAsync(AllProductsQueryViewModel queryModel)
        {
            IQueryable<Product> productsQuery = this.productsRepository.All().AsQueryable();

            if (queryModel.CurrentPage <= 0)
            {
                queryModel.CurrentPage = 1;
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SelectedCategoryId.ToString()))
            {
                productsQuery = productsQuery
                    .Where(p => p.ProductCategory.Id == queryModel.SelectedCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Description, wildCard) ||
                                EF.Functions.Like(p.Name, wildCard) ||
                                EF.Functions.Like(p.Manufacturer.Name, wildCard));
            }

            productsQuery = queryModel.ProductsSorting switch
            {
                ProductsSorting.Newest => productsQuery
                    .OrderByDescending(c => c.CreatedOn),
                ProductsSorting.Oldest => productsQuery
                    .OrderBy(c => c.CreatedOn),
                ProductsSorting.PriceAscending => productsQuery
                    .OrderBy(c => c.Price),
                ProductsSorting.PriceDescending => productsQuery
                    .OrderByDescending(c => c.Price),
                _ => productsQuery.OrderBy(c => c.CreatedOn).ThenByDescending(c => c.CreatedOn),
            };

            IEnumerable<ProductViewModel> allProducts = await productsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .To<ProductViewModel>()
                .ToArrayAsync();

            if (productsQuery.Count() != 0)
            {
                int maxPage = (int)Math.Ceiling((double)productsQuery.Count() / queryModel.ProductsPerPage);

                if (queryModel.CurrentPage > maxPage && maxPage != 0)
                {
                    throw new ArgumentException();
                }
            }

            int totalProducts = productsQuery.Count();

            return new AllProductsFilteredAndPagedServiceModel()
            {
                TotalProducts = totalProducts,
                Products = allProducts,
            };
        }

        public async Task<ProductDetailsViewModel> GetProductByIdAsync(int id)
        {
            return await this.productsRepository.All().Where(p => p.Id == id).To<ProductDetailsViewModel>().FirstOrDefaultAsync();
        }
    }
}
