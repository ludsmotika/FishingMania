namespace FishingMania.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.Comment;

    public interface ICommentService
    {
        Task PostCommentAsync(CommentInputViewModel model);

        Task<List<CommentViewModel>> GetAllCommentsForThisEntityAsync(EntityWithCommentsType entityType, int entityId);

        Task DeleteCommentByIdAsync(int id);

        Task<CommentViewModel> GetCommentByIdAsync(int id);
    }
}
