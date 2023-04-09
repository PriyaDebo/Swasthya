using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportByHospitalRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public Stream report { get; set; }
    }
}
