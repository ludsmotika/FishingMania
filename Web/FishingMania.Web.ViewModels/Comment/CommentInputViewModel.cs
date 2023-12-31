﻿namespace FishingMania.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Models;

    using static FishingMania.Data.Common.DataValidation.Comment;

    public class CommentInputViewModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        [Required]
        public int EntityTypeId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        public EntityWithCommentsType EntityType { get; set; }
    }
}
