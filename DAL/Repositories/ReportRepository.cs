using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class ReportRepository
    {
        private readonly Database database;
        private readonly Container container;

        public ReportRepository(CosmosClient client)
        {
            database = client.GetDatabase("Swasthya");
            container = database.GetContainer("Reports");
        }

        public async Task<ReportData> AddReportAsync(string email, string title, string report)
        {
            var medicalReport = new ReportData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Title = title,
                MedicalReport = report
            };

            var reportAdded = await container.CreateItemAsync<ReportData>(medicalReport);
            return reportAdded.Resource;
        }

        public async Task<IEnumerable<IReport>> GetReportsByEmailAsync(string email)
        {
            var reports = new List<IReport>();
            var query = $"SELECT * from Reports WHERE Reports.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var iterator = container.GetItemQueryIterator<ReportData>(queryDefinition);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                foreach (var report in response)
                {
                    if (report != null)
                    {
                        reports.Add(new Report()
                        {
                            Id = report.Id,
                            Email = report.Email,
                            Title = report.Title,
                            MedicalReport = report.MedicalReport
                        });
                    }
                }
            }

            return reports;
        }

        public async Task<IReport> GetReportByIdAsync(string id)
        {
            var query = $"SELECT * from Reports WHERE Report.id = @id";
            var queryDefinition = new QueryDefinition(query).WithParameter("@id", id);
            var reportResponse = container.GetItemQueryIterator<ReportData>(queryDefinition);
            var response = await reportResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            var report = new Report()
            {
                Id = responseResource.Id,
                Email = responseResource.Email,
                Title = responseResource.Title,
                MedicalReport = responseResource.MedicalReport
            };

            return report;
        }
    }
}
