namespace FishingMania.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("Vote")]
        [ActionName("Vote")]
        public async Task<IActionResult> Vote(VoteInputViewModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string voteState = string.Empty;

                    // Validate if the user has can like post.
                    if (await this.votesService.DoesVoteExistAsync(model))
                    {
                        if (model.IsClicked == true)
                        {
                            voteState = "removeVote";
                            await this.votesService.DeleteVoteAsync(model);
                        }
                        else
                        {
                            voteState = "repeatedVote";
                        }
                    }
                    else if (await this.votesService.DoesVoteExistWithOppositeEmotion(model))
                    {
                        voteState = "changingVoteIsPositive";
                        await this.votesService.ChangeVoteIsPositive(model);
                    }
                    else
                    {
                        voteState = "newVote";
                        await this.votesService.VotePostAsync(model);
                    }

                    return this.Json(voteState);
                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
