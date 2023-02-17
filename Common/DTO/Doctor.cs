using Common.Models;

namespace Common.DTO
{
    public class Doctor : IDoctor
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string SwasthyaId { get; set; }

        public string RegistrationNumber { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> PatientIds { get; set; }
    }
}
