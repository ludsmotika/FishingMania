namespace FishingMania.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Comment;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AmountInStock { get; set; }

        public int ManufacturerId { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        public Manufacturer Manufacturer { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ProductCategory ProductCategory { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
