using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.DoctorRequestModels
{
    public class RegisterDoctorRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }
    }
}
