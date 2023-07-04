﻿namespace FishingMania.Web.ViewModels.Catch
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class CatchDetailsViewModel : IMapFrom<Catch>
    {
        public string Description { get; set; }

        public decimal FishWeight { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        public int FishSpeciesId { get; set; }

        [ForeignKey(nameof(FishSpeciesId))]
        public FishSpecies FishSpecies { get; set; }

        public int FishingSpotId { get; set; }

        [ForeignKey(nameof(FishingSpotId))]
        public FishingSpot FishingSpot { get; set; }
    }
}
