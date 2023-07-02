namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        [Url]
        public string URL { get; set; }
    }
}
