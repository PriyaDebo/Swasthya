using Azure;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;
using System.Security.Cryptography;

namespace DAL.Repositories
{
    public class PatientRepository
    {
        private Database database;
        private Container container;

        public PatientRepository(CosmosClient client)
        {
            database = client.GetDatabase("Swasthya");
            container = database.GetContainer("Patient");
        }

        public async Task<Boolean> EmailExistsAsync(string email)
        {
            var query = $"SELECT Patient.email FROM Patient WHERE Patient.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = this.container.GetItemQueryIterator<string>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();
            if (response.Resource.FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }

        private string CreatePasswordHash(string password)
        {
            using(var hmac = new HMACSHA512())
            {
                var hashedPasswordByte = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                password = System.Text.Encoding.UTF8.GetString(hashedPasswordByte);
            }

            return password;
        }

        public async Task<PatientData> CreatePatientAsync(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            var individual = new PatientData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = CreatePasswordHash(password),
                Name = name,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

            var individualCreated = await container.CreateItemAsync<PatientData>(individual);
            return individualCreated.Resource;
        }
    }
}
