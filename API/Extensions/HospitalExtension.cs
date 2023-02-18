using Common.ApiResponseModels.HospitalResponseModels;
using Common.Models;

namespace API.Extensions
{
    public static class HospitalExtension
    {
        public static HospitalResponseModel ToAPIModel(this IHospital hospital)
        {
            var model = new HospitalResponseModel
            {
                Email = hospital.Email,
                Name = hospital.Name,
                PhoneNumber = hospital.PhoneNumber,
                Address = hospital.Address,
                PatientIds = hospital.PatientIds,
            };

            return model;
        }
    }
}
