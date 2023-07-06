namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FishSpeciesFishingSpots
    {
        [Required]
        public int FishSpeciesId { get; set; }

        [Required]
        [ForeignKey(nameof(FishSpeciesId))]
        public FishSpecies FishSpecies { get; set; } = null!;

        [Required]
        public int FishingSpotId { get; set; }

        [Required]
        [ForeignKey(nameof(FishingSpotId))]
        public FishingSpot FishingSpot { get; set; } = null!;
    }
}
