namespace FishingMania.Web.ViewModels.FishSpecies
{
    using System.ComponentModel.DataAnnotations;

    public class FishSpeciesDropdownViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
