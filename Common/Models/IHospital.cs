namespace Common.Models
{
    public interface IHospital
    {
        string Id { get; }

        string Email { get; }

        string Password { get; }

        string Name { get; }

        string Address { get; }

        string PhoneNumber { get; }
    }
}
