namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Comment;
    using FishingMania.Web.ViewModels.FishSpecies;

    public class FishingSpotDetailsViewModel : IMapFrom<FishingSpot>, IHaveCustomMappings
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

        public List<CatchViewModel> ThreeBiggestCatches { get; set; }

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FishingSpot, FishingSpotDetailsViewModel>()
                         .ForMember(x => x.ThreeBiggestCatches,
                                    opt => opt.MapFrom(x => x.Catches.OrderByDescending(c => c.FishWeight).Take(3)
                                                                     .Select(c => new CatchViewModel()
                                                                     {
                                                                         Id = c.Id,
                                                                         Description = c.Description,
                                                                         FishWeight = c.FishWeight,
                                                                         ImageId = c.ImageId,
                                                                         Image = c.Image,
                                                                     }).ToList()));
        }
    }
}