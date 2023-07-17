namespace FishingMania.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.Catch;

    public class Catch : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Range(typeof(decimal), MinFishWeight, MaxFishWeight)]
        public decimal FishWeight { get; set; }

        [Required]
        public int ImageId { get; set; }

        [Required]
        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        [Required]
        public int FishSpeciesId { get; set; }

        [Required]
        [ForeignKey(nameof(FishSpeciesId))]
        public FishSpecies FishSpecies { get; set; }

        [Required]
        public int FishingSpotId { get; set; }

        [Required]
        [ForeignKey(nameof(FishingSpotId))]
        public FishingSpot FishingSpot { get; set; }

        [Required]
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
