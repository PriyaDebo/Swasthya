namespace Common.Models
{
    public interface IIndividual
    {
        string Email { get; }

        byte[] PasswordHash { get; }

        byte[] PasswordSalt { get; }

        string Name { get; }

        string PhoneNumber { get; }

        string DateOfBirth { get; }
    }
}
