namespace FishingMania.Web.ViewModels.Catch
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Web.Infrastructure.Attributes;
    using FishingMania.Web.ViewModels.FishingSpot;
    using FishingMania.Web.ViewModels.FishSpecies;
    using Microsoft.AspNetCore.Http;

    using static FishingMania.Data.Common.DataValidation.Catch;

    public class CatchFormViewModel
    {
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), MinFishWeight, MaxFishWeight)]
        [DisplayName("Fish Weight")]
        public decimal Weight { get; set; }

        [Required]
        [AllowedFileExtensions]
        [DataType(DataType.Upload)]
        [DisplayName("Upload image")]
        public IFormFile ImageFile { get; set; }

        [Required]
        [DisplayName("Choose fish species")]
        public int FishSpeciesId { get; set; }

        [Required]
        public List<FishSpeciesDropdownViewModel> FishSpecies { get; set; } = new List<FishSpeciesDropdownViewModel>();

        [Required]
        [DisplayName("Choose spot")]
        public int FishingSpotId { get; set; }

        [Required]
        public List<FishingSpotDropdownViewModel> FishingSpots { get; set; } = new List<FishingSpotDropdownViewModel>();
    }
}
