﻿namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize]
    [Route("api/[controller]/{id?}")]
    public class CommentsController : Controller
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
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.commentsService.PostCommentAsync(model);

                    // Getting and returning all the comments so the user see the new comments that may be posted
                    List<CommentViewModel> viewModel = await this.commentsService.GetAllCommentsForThisEntityAsync(model.EntityType, model.EntityTypeId);
                    return this.PartialView("~/Views/Shared/Comments/_CommentsListPartialView.cshtml", viewModel);
                }

                return this.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public async Task Delete(int id)
        {
            try
            {
                CommentViewModel viewModel = await this.commentsService.GetCommentByIdAsync(id);

                if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) == viewModel.ApplicationUserId)
                {
                    await this.commentsService.DeleteCommentByIdAsync(id);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
