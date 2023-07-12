namespace FishingMania.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post(CommentInputViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                CommentViewModel viewModel = await this.commentsService.PostComment(model);
                return this.PartialView("~/Views/Shared/Comments/_CommentPartialView.cshtml", viewModel);
            }

            return this.BadRequest();
        }
    }
}
