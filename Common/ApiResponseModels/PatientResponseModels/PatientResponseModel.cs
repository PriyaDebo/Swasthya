using Common.ApiResponseModels.DoctorResponseModels;

namespace Common.ApiResponseModels.PatientResponseModels
{
    public class PatientResponseModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string SwasthyaId { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public List<DoctorReferenceModel> Doctors { get; set; }
    }
}
