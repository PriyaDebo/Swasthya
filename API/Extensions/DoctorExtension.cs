using Common.ApiResponseModels.DoctorResponseModels;
using Common.ApiResponseModels.PatientResponseModels;
using Common.Models;

namespace API.Extensions
{
    public static class DoctorExtension
    {
        public static DoctorResponseModel ToAPIModel(this IDoctor doctor)
        {
            var model = new DoctorResponseModel
            {
                Email = doctor.Email,
                Name = doctor.Name,
                SwasthyaId = doctor.SwasthyaId,
                PhoneNumber = doctor.PhoneNumber,
                RegistrationNumber = doctor.RegistrationNumber,
            };

            if (doctor.Patients == null)
            {
                return model;
            }

            if (model.Patients == null)
            {
                model.Patients = new List<PatientReferenceModel>();
            }

            foreach (var patient in doctor.Patients)
            {
                model.Patients.Add(patient.BasicInfoToAPIModel());
            }

            return model;
        }

        public static DoctorReferenceModel BasicInfoToAPIModel(this IDoctor doctor)
        {
            var model = new DoctorReferenceModel
            {
                Email = doctor.Email,
                Name = doctor.Name,
                SwasthyaId = doctor.SwasthyaId,
                PhoneNumber = doctor.PhoneNumber,
                RegistrationNumber = doctor.RegistrationNumber,
            };

            return model;
        }
    }
}
