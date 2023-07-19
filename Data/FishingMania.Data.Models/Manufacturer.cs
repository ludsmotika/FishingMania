namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.Manufacturer;

    public class Manufacturer : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }
    }
}
