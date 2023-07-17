namespace FishingMania.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class VoteViewModel : IMapFrom<Vote>
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        public bool IsPositive { get; set; }
    }
}
