namespace Common.Models
{
    public interface IPatient
    {
        string Id { get; }

        string Email { get; }

        string Password { get; }

        string Name { get; }

        string SwasthyaId { get; }

        string PhoneNumber { get; }

        string DateOfBirth { get; }

        List<string> DoctorIds { get; }

        List<IDoctor> Doctors { get; }
    }
}
