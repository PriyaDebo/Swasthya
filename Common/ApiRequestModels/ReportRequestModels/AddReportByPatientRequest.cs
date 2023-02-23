using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportByPatientRequest
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string report { get; set; }
    }
}
