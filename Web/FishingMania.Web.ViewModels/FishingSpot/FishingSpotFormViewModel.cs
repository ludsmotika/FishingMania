namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Models;
    using FishingMania.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    using static FishingMania.Data.Common.DataValidation.FishingSpot;

    public class FishingSpotFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public FishingSpotType FishingSpotType { get; set; }

        [Required]
        [AllowedFileExtensions]
        [DataType(DataType.Upload)]
        [DisplayName("Upload image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<FishSpecies> FishSpecies { get; set; } = new HashSet<FishSpecies>();
    }
}
