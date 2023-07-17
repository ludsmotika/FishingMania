namespace FishingMania.Services.Data.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Votes;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task ChangeVoteIsPositive(VoteInputViewModel model)
        {
            Vote voteToEdit = await this.votesRepository.All().Where(v => v.ApplicationUserId == model.ApplicationUserId && v.IsPositive == !model.IsPositive && v.CatchId == model.CatchId).FirstOrDefaultAsync();

            voteToEdit.IsPositive = model.IsPositive;

            await this.votesRepository.SaveChangesAsync();
        }

        public async Task DeleteVoteAsync(VoteInputViewModel model)
        {
            Vote voteToDelete = await this.votesRepository.All().Where(v => v.ApplicationUserId == model.ApplicationUserId && v.IsPositive == model.IsPositive && v.CatchId == model.CatchId).FirstOrDefaultAsync();

            this.votesRepository.Delete(voteToDelete);
            await this.votesRepository.SaveChangesAsync();
        }

        public async Task<bool> DoesVoteExistAsync(VoteInputViewModel model)
        {
            return await this.votesRepository.All().Where(v => v.ApplicationUserId == model.ApplicationUserId && v.IsPositive == model.IsPositive && v.CatchId == model.CatchId).AnyAsync();
        }

        public async Task<bool> DoesVoteExistWithOppositeEmotion(VoteInputViewModel model)
        {
            return await this.votesRepository.All().Where(v => v.ApplicationUserId == model.ApplicationUserId && v.IsPositive == !model.IsPositive && v.CatchId == model.CatchId).AnyAsync();
        }

        public async Task VotePostAsync(VoteInputViewModel model)
        {
            Vote voteDeletedVoteWithSameProps = await this.votesRepository.AllWithDeleted().Where(v => v.ApplicationUserId == model.ApplicationUserId && v.IsPositive == model.IsPositive && v.CatchId == model.CatchId).FirstOrDefaultAsync();

            if (voteDeletedVoteWithSameProps != null)
            {
                voteDeletedVoteWithSameProps.IsDeleted = false;
                voteDeletedVoteWithSameProps.DeletedOn = null;
            }
            else
            {
                Vote newVote = new Vote()
                {
                    ApplicationUserId = model.ApplicationUserId,
                    CatchId = model.CatchId,
                    IsPositive = model.IsPositive,
                };

                await this.votesRepository.AddAsync(newVote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
