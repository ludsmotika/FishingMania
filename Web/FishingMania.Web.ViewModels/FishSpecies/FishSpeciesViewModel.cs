namespace FishingMania.Web.ViewModels.FishSpecies
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;

    public class FishSpeciesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
    }
}
