namespace FishingMania.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.Comment;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        [Required]
        public int EntityTypeId { get; set; }

        [Required]
        public EntityWithCommentsType EntityType { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
