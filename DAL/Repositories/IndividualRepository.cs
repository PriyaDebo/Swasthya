using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;
using System.Security.Cryptography;

namespace DAL.Repositories
{
    public class IndividualRepository
    {
        private Database database;
        private Container container;

        public IndividualRepository(CosmosClient client)
        {
            database = client.GetDatabase("Swasthya");
            container = database.GetContainer("Individual");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<IndividualData> CreateIndividualAsync(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var individual = new IndividualData()
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = name,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

            var individualCreated = await container.CreateItemAsync<IndividualData>(individual);
            return individualCreated.Resource;
        }
    }
}
