namespace FishingMania.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.FishingSpot;

    public class FishingSpot : BaseDeletableModel<int>
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
        public int ImageId { get; set; }

        [Required]
        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        public IEnumerable<FishSpeciesFishingSpots> FishSpeciesFishingSpots { get; set; } = new List<FishSpeciesFishingSpots>();

        public IEnumerable<Catch> Catches { get; set; } = new List<Catch>();
    }
}
