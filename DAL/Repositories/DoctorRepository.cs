using BCrypt.Net;
using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class DoctorRepository
    {
        private Database database;
        private Container container;

        public DoctorRepository(CosmosClient client)
        {
            this.database = client.GetDatabase("Swasthya");
            this.container = database.GetContainer("Doctor");
        }

        public async Task<Boolean> EmailExistsAsync(string email)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = this.container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        public async Task<Boolean> RegistrationNumberExistsAsync(string registrationNumber)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.registrationNumber = @registrationNumber";
            var queryDefinition = new QueryDefinition(query).WithParameter("@registrationNumber", registrationNumber);
            var registrationNumberResponse = this.container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await registrationNumberResponse.ReadNextAsync();

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

        public async Task<DoctorData> RegisterDoctorAsync(string email, string password, string name, string registrationNumber, string phoneNumber)
        {
            var doctor = new DoctorData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = CreatePasswordHash(password),
                Name = name,
                RegistrationNumber = registrationNumber,
                PhoneNumber = phoneNumber
            };

            var doctorData = await container.CreateItemAsync<DoctorData>(doctor);
            return doctorData;
        }

        public async Task<IDoctor> LoginDoctorAsync(string email, string password)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var doctorResponse = this.container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await doctorResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, responseResource.Password))
            {
                return null;
            }

            var doctor = new Doctor()
            {
                Name = responseResource.Name,
                Email = responseResource.Email,
                PhoneNumber = responseResource.PhoneNumber,
                RegistrationNumber = responseResource.RegistrationNumber,
            };

            return doctor;
        }
    }
}
