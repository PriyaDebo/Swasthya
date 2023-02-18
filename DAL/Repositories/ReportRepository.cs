using Azure.Storage.Blobs;
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
        private readonly BlobContainerClient blobContainerClient;

        public ReportRepository(CosmosClient client, BlobServiceClient blobServiceClient)
        {
            this.database = client.GetDatabase("Swasthya");
            this.container = database.GetContainer("Reports");
            this.blobContainerClient = blobServiceClient.GetBlobContainerClient("reports");
        }

        public async Task<ReportData> AddReportAsync(string email, string title, Stream report)
        {
            var blobName = Guid.NewGuid().ToString();
            var blob = blobContainerClient.GetBlobClient(blobName);
            await blob.UploadAsync(report);

            var medicalReport = new ReportData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Title = title,
                MedicalReport = blobName
            };

            var reportAdded = await container.CreateItemAsync<ReportData>(medicalReport);
            return reportAdded.Resource;
        }

        public async Task<Stream> GetReportByBlobNameAsync(string blobName)
        {
            Stream report = new MemoryStream();
            var blob = blobContainerClient.GetBlobClient(blobName);
            if (await blob.ExistsAsync())
            {
                await blob.UploadAsync(report);
            }

            return report;
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
                            Title = report.Title,
                        });
                    }
                }
            }

            return reports;
        }

        //public async Task<object> ReportExists(string reference)
        //{
        //    Stream rep;
        //    var blob = blobContainerClient.GetBlobClient("guid");
        //    var report = await blob.UploadAsync(rep);
        //    var blobUri = blob.Uri.AbsoluteUri;
        //    await blob.DownloadToAsync(rep);
        //}

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
