using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ReportData : IReport
    {
        public ReportData()
        {
        }

        public ReportData(ReportData reportData)
        {
            Id = reportData.Id;
            Email = reportData.Email;
            Title = reportData.Title;
            MedicalReport = reportData.MedicalReport;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "medicalReport")]
        public string MedicalReport { get; set; }
    }
}
