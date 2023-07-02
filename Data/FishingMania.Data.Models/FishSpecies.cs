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

        public List<FishSpeciesFishingSpots> FishSpeciesFishingSpots { get; set; } = new List<FishSpeciesFishingSpots>();
    }
}
