namespace FishingMania.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    public class FishSpeciesFishingSpots : BaseDeletableModel<int>
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
