namespace FishingMania.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputViewModel
    {
        [Required]
        public int CatchId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        public bool IsClicked { get; set; }

        [Required]
        public bool IsPositive { get; set; }
    }
}
