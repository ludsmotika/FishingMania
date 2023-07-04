namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class FishingSpotViewModel : IMapFrom<FishingSpot>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
    }
}
