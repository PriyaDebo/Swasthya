using Common.ApiResponseModels;
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
            };

            return model;
        }
    }
}
