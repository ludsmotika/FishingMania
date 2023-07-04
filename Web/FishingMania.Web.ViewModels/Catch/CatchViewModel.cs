namespace FishingMania.Web.ViewModels.Catch
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class CatchViewModel : IMapFrom<Catch>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal FishWeight { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
    }
}
