namespace FishingMania.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Web.ViewModels.Product.Enums;
    using FishingMania.Web.ViewModels.ProductCategory;

    using static FishingMania.Common.GlobalConstants;

    public class AllProductsQueryViewModel
    {
        public AllProductsQueryViewModel()
        {
            this.CurrentPage = DefaultPage;
            this.ProductsPerPage = EntitiesPerPage;

            this.Products = new HashSet<ProductViewModel>();
        }

        [Display(Name = "Category")]
        public int? SelectedCategoryId { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Products By")]
        public ProductsSorting ProductsSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Products Per Page")]
        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
