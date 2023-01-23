namespace Common.ApiRequestModels.DoctorRequestModels
{
    public class RegisterDoctorRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
