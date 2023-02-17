namespace Common.Models
{
    public interface IDoctor
    {
        string Id { get; }

        string Email { get; }

        string Password { get; }

        string Name { get; }

        string SwasthyaId { get; }

        string RegistrationNumber { get; }

        string PhoneNumber { get; }

        List<string> PatientIds { get; }

    }
}
