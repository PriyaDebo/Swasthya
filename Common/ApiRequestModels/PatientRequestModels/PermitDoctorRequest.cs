using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.PatientRequestModels
{
    public class PermitDoctorRequest
    {
        //[Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

        [Required]
        public string doctorSwasthyaId { get; set; }
    }
}
