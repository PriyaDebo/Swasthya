using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportByPatientRequest
    {
        [Required]
        public string title { get; set; }

        [Required]
        public Stream report { get; set; }
    }
}
