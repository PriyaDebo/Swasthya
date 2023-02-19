using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.ReportRequestModels
{
    public class GetReportByBlobNameRequest
    {
        [Required]
        public string blobName { get; set; }
    }
}
