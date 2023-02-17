using Common.ApiResponseModels;
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
                PatientIds = doctor.PatientIds,
            };

            return model;
        }
    }
}
