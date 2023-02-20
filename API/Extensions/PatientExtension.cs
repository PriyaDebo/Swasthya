using Common.ApiResponseModels.DoctorResponseModels;
using Common.ApiResponseModels.PatientResponseModels;
using Common.Models;

namespace API.Extensions
{
    public static class PatientExtension
    {
        public static PatientResponseModel ToAPIModel(this IPatient patient)
        {
            var model = new PatientResponseModel
            {
                Email = patient.Email,
                Name = patient.Name,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                SwasthyaId = patient.SwasthyaId,
            };

            if (patient.Doctors == null)
            {
                return model;
            }

            if (model.Doctors == null)
            {
                model.Doctors = new List<DoctorReferenceModel>();
            }

            foreach (var doctor in patient.Doctors)
            {
                model.Doctors.Add(doctor.BasicInfoToAPIModel());
            }

            return model;
        }

        public static PatientReferenceModel BasicInfoToAPIModel(this IPatient patient)
        {
            var model = new PatientReferenceModel
            {
                Email = patient.Email,
                Name = patient.Name,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                SwasthyaId = patient.SwasthyaId,
            };

            return model;
        }
    }
}
