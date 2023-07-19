namespace FishingMania.Services.Data.ServiceModels
{
    using System.Collections.Generic;

    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Product;

    public class AllProductsFilteredAndPagedServiceModel
    {
        public AllProductsFilteredAndPagedServiceModel()
        {
            this.Products = new HashSet<ProductViewModel>();
        }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
