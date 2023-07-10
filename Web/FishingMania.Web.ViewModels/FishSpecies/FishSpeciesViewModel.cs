namespace FishingMania.Web.ViewModels.FishSpecies
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class FishSpeciesViewModel : IMapFrom<FishSpecies>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
    }
}
