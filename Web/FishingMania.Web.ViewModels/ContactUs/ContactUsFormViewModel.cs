namespace FishingMania.Web.ViewModels.ContactUs
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.ContactUs.Enums;

    public class ContactUsFormViewModel
    {
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public ComplainTopic Topic { get; set; }
    }
}
