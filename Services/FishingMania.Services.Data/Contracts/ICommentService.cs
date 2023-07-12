namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.Comment;

    public interface ICommentService
    {
        Task<CommentViewModel> PostComment(CommentInputViewModel model);

        Task<List<CommentViewModel>> GetAllCommentsForThisEntity(EntityWithCommentsType entityType, int entityId);
    }
}
