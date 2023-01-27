using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

namespace DAL.Repositories
{
    public class DoctorRepository
    {
        private readonly Database database;
        private readonly Container container;

        public DoctorRepository(CosmosClient client)
        {
            database = client.GetDatabase("Swasthya");
            container = database.GetContainer("Doctor");
        }

        public async Task<Boolean> EmailExistsAsync(string email)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        public async Task<Boolean> RegistrationNumberExistsAsync(string registrationNumber)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.registrationNumber = @registrationNumber";
            var queryDefinition = new QueryDefinition(query).WithParameter("@registrationNumber", registrationNumber);
            var registrationNumberResponse = container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await registrationNumberResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        public async Task<DoctorData> CreateDoctorAsync(string email, string password, string name, string swasthyaId, string registrationNumber, string phoneNumber)
        {
            var doctor = new DoctorData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = password,
                SwasthyaId = swasthyaId,
                Name = name,
                RegistrationNumber = registrationNumber,
                PhoneNumber = phoneNumber
            };

            var doctorData = await container.CreateItemAsync<DoctorData>(doctor);
            return doctorData;
        }

        public async Task<IDoctor> GetDoctorAsync(string email)
        {
            var query = $"SELECT * FROM Doctor WHERE Doctor.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var doctorResponse = container.GetItemQueryIterator<DoctorData>(queryDefinition);
            var response = await doctorResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            var doctor = new Doctor()
            {
                Name = responseResource.Name,
                Email = responseResource.Email,
                Password = responseResource.Password,
                SwasthyaId = responseResource.SwasthyaId,
                PhoneNumber = responseResource.PhoneNumber,
                RegistrationNumber = responseResource.RegistrationNumber,
            };

            return doctor;
        }
    }
}
