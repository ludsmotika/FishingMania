namespace FishingMania.Web.ViewModels.Report
{
    using System.ComponentModel.DataAnnotations;

    using static FishingMania.Data.Common.DataValidation.Report;

    public class ReportInputViewModel
    {
        [Required]
        [StringLength(ComplainMaxLength, MinimumLength = ComplainMinLength)]
        public string Complain { get; set; }

        [Required]
        public int CatchId { get; set; }
    }
}
