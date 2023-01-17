namespace Common.Models
{
    public interface IPatient
    {
        string Id { get; }

        string Email { get; }

        string Password { get; }

        string Name { get; }

        string PhoneNumber { get; }

        string DateOfBirth { get; }
    }
}
