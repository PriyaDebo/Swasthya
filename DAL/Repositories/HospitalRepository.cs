using BCrypt.Net;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class HospitalRepository
    {
        private Database database;
        private Container container;

        public HospitalRepository(CosmosClient client)
        {
            this.database = client.GetDatabase("Swasthya");
            this.container = database.GetContainer("Hospital");
        }

        public async Task<Boolean> EmailExistsAsync(string email)
        {
            var query = $"SELECT * FROM Hospital WHERE Hospital.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = this.container.GetItemQueryIterator<HospitalData>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        private string CreatePasswordHash(string password)
        {
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA512);
            return password;
        }

        private bool VerifyPasswordHash(string passwordInput, string passwordOriginal)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(passwordInput, passwordOriginal, hashType: HashType.SHA512);
        }

        public async Task<HospitalData> RegisterHospitalAsync(string email, string password, string name, string address, string phoneNumber)
        {
            var hospital = new HospitalData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = CreatePasswordHash(password),
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber
            };

            var doctorRegistered = await container.CreateItemAsync<HospitalData>(hospital);
            return doctorRegistered.Resource;
        }

        public async Task<IHospital> LoginHospitalAsync(string email, string password)
        {
            var query = $"SELECT * FROM Hospital WHERE Hospital.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var hospitalResponse = this.container.GetItemQueryIterator<HospitalData>(queryDefinition);
            var response = await hospitalResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, responseResource.Password))
            {
                return null;
            }

            var hospital = new HospitalData()
            {
                Email = responseResource.Email,
                Name = responseResource.Name,
                Address = responseResource.Address,
                PhoneNumber = responseResource.PhoneNumber,
            };

            return hospital;
        }
    }
}
