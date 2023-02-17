namespace Common.ApiResponseModels
{
    public class HospitalResponseModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<string> PatientIds { get; set; }
    }
}
