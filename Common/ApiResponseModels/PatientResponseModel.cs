namespace Common.ApiResponseModels
{
    public class PatientResponseModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string SwasthyaId { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public List<string> DoctorIds { get; set; }
    }
}
