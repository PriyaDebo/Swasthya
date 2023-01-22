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
                PhoneNumber = doctor.PhoneNumber,
                RegistrationNumber = doctor.RegistrationNumber,
            };

            return model;
        }
    }
}
