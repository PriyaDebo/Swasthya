using Common.Models;

namespace Common.DTO
{
    public class Individual : IIndividual
    {
        public string Email { get; set;  }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }
    }
}
