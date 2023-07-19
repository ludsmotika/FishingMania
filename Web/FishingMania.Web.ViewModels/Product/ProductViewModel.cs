namespace FishingMania.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<Image> Images { get; set; }
    }
}
