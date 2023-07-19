namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.ProductCategory;

    public class ProductCategory : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
