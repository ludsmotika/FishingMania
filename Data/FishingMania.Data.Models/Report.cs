namespace FishingMania.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.Report;

    public class Report : BaseDeletableModel<Guid>
    {
        [Required]
        [StringLength(ComplainMaxLength, MinimumLength = ComplainMinLength)]
        public string Complain { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int CatchId { get; set; }

        [Required]
        [ForeignKey(nameof(CatchId))]
        public Catch Catch { get; set; }
    }
}
