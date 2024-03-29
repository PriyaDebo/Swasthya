﻿using Common.ApiResponseModels.PatientResponseModels;

namespace Common.ApiResponseModels.HospitalResponseModels
{
    public class HospitalResponseModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<PatientReferenceModel> Patients { get; set; }
    }
}
