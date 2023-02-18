﻿using System.ComponentModel.DataAnnotations;

namespace Common.ApiRequestModels.PatientRequestModels
{
    public class RegisterPatientRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string DateOfBirth { get; set; }
    }
}
