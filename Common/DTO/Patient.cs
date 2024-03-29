﻿using Common.Models;

namespace Common.DTO
{
    public class Patient : IPatient
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string SwasthyaId { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public List<string> DoctorIds { get; set; }

        public List<IDoctor> Doctors { get; set; }
    }
}
