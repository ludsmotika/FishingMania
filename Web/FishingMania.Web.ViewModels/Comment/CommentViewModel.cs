namespace FishingMania.Web.ViewModels.Comment
{
    using System;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string ApplicationUserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
