namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using FishingMania.Web.ViewModels.Votes;

    public interface IVotesService
    {
        Task VotePostAsync(VoteInputViewModel model);

        Task<bool> DoesVoteExistAsync(VoteInputViewModel model);

        Task DeleteVoteAsync(VoteInputViewModel model);

        Task<bool> DoesVoteExistWithOppositeEmotion(VoteInputViewModel model);

        Task ChangeVoteIsPositive(VoteInputViewModel model);
    }
}
