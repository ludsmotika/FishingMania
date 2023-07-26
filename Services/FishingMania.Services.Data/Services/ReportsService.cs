namespace FishingMania.Services.Data.Services
{
    using System.Threading.Tasks;

    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;

    public class ReportsService : IReportsService
    {
        private readonly IDeletableEntityRepository<Report> reportsRepository;

        public ReportsService(IDeletableEntityRepository<Report> reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        public async Task ReportCatch(int catchId, string applicationUserId, string complain)
        {
            Report reportToAdd = new Report()
            {
                ApplicationUserId = applicationUserId,
                Complain = complain,
                CatchId = catchId,
            };

            await this.reportsRepository.AddAsync(reportToAdd);
            await this.reportsRepository.SaveChangesAsync();
        }
    }
}
