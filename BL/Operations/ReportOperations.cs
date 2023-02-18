using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class ReportOperations
    {
        ReportRepository reportRepository;

        public ReportOperations(ReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<IReport> AddReportAsync(string email, string title, Stream report)
        {
            var reportResponse = await reportRepository.AddReportAsync(email, title, report);
            return reportResponse;
        }

        public async Task<IReport> GetReportByIdAsync(string id)
        {
            var reportResponse = await reportRepository.GetReportByIdAsync(id);
            return reportResponse;
        }

        public async Task<IEnumerable<IReport>> GetReportsByEmailAsync(string email)
        {
            var reportsResponse = await reportRepository.GetReportsByEmailAsync(email);
            return reportsResponse;
        }

        public async Task<Stream> GetReportByBlobNameAsync(string blobName)
        {
            var report = await reportRepository.GetReportByBlobNameAsync(blobName);
            return report;
        }
    }
}
