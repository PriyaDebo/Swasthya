namespace Common.ApiResponseModels
{
    public class DoctorResponseModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string SwasthyaId { get; set; }

        public string RegistrationNumber { get; set; }

        public List<string> PatientIds { get; set; }
    }
}
