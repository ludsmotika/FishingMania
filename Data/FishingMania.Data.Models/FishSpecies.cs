namespace FishingMania.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.FishSpecies;

    public class FishSpecies : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public FishType Type { get; set; }

        [Required]
        public int ImageId { get; set; }

        [Required]
        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        public List<FishingSpot> FishingSpots { get; set; } = new List<FishingSpot>();
    }
}
