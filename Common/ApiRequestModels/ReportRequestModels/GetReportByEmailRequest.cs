using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.ReportRequestModels
{
    public class GetReportByEmailRequest
    {
        [Required]
        public string email { get; set; }
    }
}
