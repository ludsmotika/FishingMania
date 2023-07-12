namespace FishingMania.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SendGrid.Helpers.Mail;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {

        private readonly ICommentService commentsService;

        public CommentsController(ICommentService commentService)
        {
            this.commentsService = commentService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post(CommentInputViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.commentsService.PostComment(model);

                List<CommentViewModel> viewModel = await this.commentsService.GetAllCommentsForThisEntity(model.EntityType, model.EntityTypeId);
                return this.PartialView("~/Views/Shared/Comments/_CommentsListPartialView.cshtml", viewModel);
            }

            return this.BadRequest();
        }
    }
}
