﻿using Common.ApiResponseModels.PatientResponseModels;

namespace Common.ApiResponseModels.DoctorResponseModels
{
    public class DoctorResponseModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string SwasthyaId { get; set; }

        public string RegistrationNumber { get; set; }

        public List<PatientReferenceModel> Patients { get; set; }
    }
}
