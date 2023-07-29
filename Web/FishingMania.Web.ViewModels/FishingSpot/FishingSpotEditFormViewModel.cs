namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    using static FishingMania.Data.Common.DataValidation.FishingSpot;

    public class FishingSpotEditFormViewModel : IMapFrom<FishingSpot>
    {
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public FishingSpotType FishingSpotType { get; set; }

        [AllowedFileExtensions]
        [DataType(DataType.Upload)]
        [DisplayName("Upload image")]
        public IFormFile ImageFile { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
