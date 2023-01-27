using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels
{
    public abstract class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
