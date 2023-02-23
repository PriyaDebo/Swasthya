using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.HospitalRequestModels
{
    public class GetPatientPermitRequest
    {
        //[Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address")]
        public string email { get; set; }

        [Required]
        public string patientSwasthyaId { get; set; }
    }
}
