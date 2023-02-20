using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.PatientRequestModels
{
    public class RegisterPatientRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(\+\d{1,3}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Please enter a valid 10 digit phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string DateOfBirth { get; set; }
    }
}
