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

        public async Task DeleteCommentByIdAsync(int id)
        {
            Comment commentToDelete = await this.commentRepository.All().FirstOrDefaultAsync(c => c.Id == id);
            if (commentToDelete == null)
            {
                throw new ArgumentException();
            }

            this.commentRepository.Delete(commentToDelete);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<List<CommentViewModel>> GetAllCommentsForThisEntityAsync(EntityWithCommentsType entityType, int entityId)
        {
            List<CommentViewModel> comments = await this.commentRepository.All().Where(x => x.EntityTypeId == entityId && x.EntityType == entityType).OrderByDescending(x => x.CreatedOn).To<CommentViewModel>().ToListAsync();
            return comments;
        }

        public async Task<CommentViewModel> GetCommentByIdAsync(int id)
        {
            CommentViewModel comment = await this.commentRepository.All().Where(c => c.Id == id).To<CommentViewModel>().FirstOrDefaultAsync();

            if (comment == null)
            {
                throw new NullReferenceException();
            }

            return comment;
        }

        public async Task PostCommentAsync(CommentInputViewModel model)
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
