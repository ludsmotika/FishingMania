namespace FishingMania.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IReportsService
    {
        Task ReportCatch(int catchId, string applicationUserId, string complain);
    }
}
