using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.PatientRequestModels
{
    public class PermitDoctorRequest
    {
        [Required]
        public string doctorSwasthyaId { get; set; }
    }
}
