namespace FishingMania.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Services.Data.Services;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Comment;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IDeletableEntityRepository<Comment> commentsRepository;

        private CommentService commentService;

        public CommentServiceTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FishingManiaComments")
               .Options;

            this.applicationDbContext = new ApplicationDbContext(contextOptions);

            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();

            this.commentsRepository = new EfDeletableEntityRepository<Comment>(this.applicationDbContext);
            this.commentService = new CommentService(this.commentsRepository);
        }

        [Fact]
        public async Task GetCommentByIdAsyncWorkWithValidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(CommentViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var comment = await this.commentService.GetCommentByIdAsync(1);

            Assert.NotNull(comment);
        }

        [Fact]
        public async Task GetCommentByIdAsyncWorkWithInvalidId()
        {
            AutoMapperConfig.RegisterMappings(typeof(CommentViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            async Task GetComment() => await this.commentService.GetCommentByIdAsync(4);

            await Assert.ThrowsAsync<NullReferenceException>(GetComment);
        }

        [Fact]
        public async Task DeleteCommentByIdAsyncWorksWithValidId()
        {
            this.SeedDataAsync();

            await this.commentService.DeleteCommentByIdAsync(1);

            var commentsCount = await this.commentsRepository.All().CountAsync();

            Assert.Equal(2, commentsCount);
        }

        [Fact]
        public async Task DeleteCommentByIdAsyncWorksWithInvalidId()
        {
            this.SeedDataAsync();

            async Task DeleteComment() => await this.commentService.DeleteCommentByIdAsync(4);

            await Assert.ThrowsAsync<ArgumentException>(DeleteComment);
        }

        [Fact]
        public async Task GetAllCommentsForThisEntityAsyncWithValidInput()
        {
            AutoMapperConfig.RegisterMappings(typeof(CommentViewModel).GetTypeInfo().Assembly);

            this.SeedDataAsync();

            var comments = await this.commentService.GetAllCommentsForThisEntityAsync(EntityWithCommentsType.Catch, 1);

            Assert.Single(comments);
        }

        [Fact]
        public async Task PostCommentAsyncWorksCorrect()
        {
            this.SeedDataAsync();

            var commentViewModel = new CommentInputViewModel()
            {
                Content = "Test",
                EntityTypeId = 1,
                EntityType = EntityWithCommentsType.Catch,
                ApplicationUserId = "testUserId",
            };

            await this.commentService.PostCommentAsync(commentViewModel);

            Assert.Equal(4, this.commentsRepository.All().Count());
        }

        private async Task SeedDataAsync()
        {
            var firstCatch = new Catch()
            {
                Id = 1,
                Description = "Test",
                FishWeight = 1.95m,
                ApplicationUserId = "testUserId",
            };

            var firstComment = new Comment()
            {
                Id = 1,
                Content = "Test Content",
                ApplicationUserId = "testUserId",
                EntityTypeId = 0,
                EntityType = EntityWithCommentsType.Catch,
            };

            var secondComment = new Comment()
            {
                Id = 2,
                Content = "Test Content",
                ApplicationUserId = "testUserId",
                EntityTypeId = 1,
                EntityType = EntityWithCommentsType.Catch,
            };

            var thirdComment = new Comment()
            {
                Id = 3,
                Content = "Test Content",
                ApplicationUserId = "testUserId",
                EntityTypeId = 2,
                EntityType = EntityWithCommentsType.Catch,
            };

            await this.applicationDbContext.Catches.AddAsync(firstCatch);
            await this.commentsRepository.AddAsync(firstComment);
            await this.commentsRepository.AddAsync(secondComment);
            await this.commentsRepository.AddAsync(thirdComment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
