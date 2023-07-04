namespace FishingMania.Web.ViewModels.FishingSpot
{
    using System.ComponentModel.DataAnnotations;

    public class FishingSpotDropdownViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
