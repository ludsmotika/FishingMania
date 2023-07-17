namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int CatchId { get; set; }

        [Required]
        [ForeignKey(nameof(CatchId))]
        public Catch Catch { get; set; }

        [Required]
        public bool IsPositive { get; set; }
    }
}
