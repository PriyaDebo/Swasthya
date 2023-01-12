namespace Common.Models
{
    public interface IIndividual
    {
        string Email { get; }

        string Name { get; }

        string PhoneNumber { get; }

        string DateOfBirth { get; }
    }
}
