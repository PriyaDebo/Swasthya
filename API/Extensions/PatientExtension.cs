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
                DoctorIds = patient.DoctorIds,
            };

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
