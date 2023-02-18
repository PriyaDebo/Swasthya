using Common.ApiResponseModels.HospitalResponseModels;
using Common.ApiResponseModels.PatientResponseModels;
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
            };

            if (hospital.PatientIds != null )
            {
                if (model.Patients == null)
                {
                    model.Patients = new List<PatientReferenceModel>();
                }

                foreach (var patient in hospital.Patients)
                {
                    model.Patients.Add(patient.BasicInfoToAPIModel());
                }
            }

            return model;
        }
    }
}
