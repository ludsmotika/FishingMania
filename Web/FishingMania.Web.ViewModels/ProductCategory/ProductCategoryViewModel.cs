namespace FishingMania.Web.ViewModels.ProductCategory
{
    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class ProductCategoryViewModel : IMapFrom<ProductCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
