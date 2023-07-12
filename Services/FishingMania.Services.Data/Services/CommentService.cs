namespace FishingMania.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Catch;
    using FishingMania.Web.ViewModels.Comment;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {

        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<List<CommentViewModel>> GetAllCommentsForThisEntity(EntityWithCommentsType entityType, int entityId)
        {
            List<CommentViewModel> comments = await this.commentRepository.All().Where(x => x.EntityTypeId == entityId && x.EntityType == entityType).OrderByDescending(x => x.CreatedOn).To<CommentViewModel>().ToListAsync();
            return comments;
        }

        public async Task PostComment(CommentInputViewModel model)
        {

            Comment commentToAdd = new Comment()
            {
                Content = model.Content,
                ApplicationUserId = model.ApplicationUserId,
                EntityType = model.EntityType,
                EntityTypeId = model.EntityTypeId,
                CreatedOn = DateTime.Now,
            };

            await this.commentRepository.AddAsync(commentToAdd);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
