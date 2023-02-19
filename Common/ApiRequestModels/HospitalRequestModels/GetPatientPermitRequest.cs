using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.HospitalRequestModels
{
    public class GetPatientPermitRequest
    {
        [Required]
        public string PatientSwasthyaId { get; set; }
    }
}
