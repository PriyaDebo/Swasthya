namespace Common.ApiRequestModels.HospitalRequestModels
{
    public class RegisterHospitalRequest
    {
        public string Email { get; set; }

        public string Password { get;set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
