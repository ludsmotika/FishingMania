namespace FishingMania.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    using static FishingMania.Data.Common.DataValidation.Product;

    public class ProductFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range(MinAmountInStock, MaxAmountInStock)]
        [DisplayName("Amount in stock")]
        public int AmountInStock { get; set; }

        [Required]
        [Range(typeof(decimal), MinPrice, MaxPrice)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }

        [Required]
        [ForeignKey(nameof(ManufacturerId))]
        public List<Manufacturer> Manufacturer { get; set; } = new List<Manufacturer>();

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public List<ProductCategory> Category { get; set; } = new List<ProductCategory>();

        [Required]
        [AllowedFileExtensions]
        [DataType(DataType.Upload)]
        [DisplayName("Upload images")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

    }
}
