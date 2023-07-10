namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.FishSpecies;

    public class FishingSpotDetailsViewModel : IMapFrom<FishingSpot>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        public List<FishSpeciesViewModel> FishSpecies { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<FishingSpot, FishingSpotDetailsViewModel>().ForMember(x => x.FishSpecies, opt => opt.MapFrom(x => x.FishSpeciesFishingSpots
        //                                 .Select(fs => new FishSpeciesViewModel()
        //                                 {
        //                                     Image = fs.FishSpecies.Image,
        //                                     Name = fs.FishSpecies.Name,
        //                                 }).ToList()));
        //}
    }
}
