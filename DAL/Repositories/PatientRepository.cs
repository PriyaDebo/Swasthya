using Common.DTO;
using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

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
            var query = $"SELECT * FROM Patient WHERE Patient.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = container.GetItemQueryIterator<PatientData>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        public async Task<PatientData> CreatePatientAsync(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            var patient = new PatientData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = password,
                Name = name,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

            var patientRegistered = await container.CreateItemAsync<PatientData>(patient);
            return patientRegistered.Resource;
        }

        public async Task<IPatient> GetPatientAsync(string email)
        {
            var query = $"SELECT * FROM Patient WHERE Patient.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var patientResponse = container.GetItemQueryIterator<PatientData>(queryDefinition);
            var response = await patientResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            var patient = new Patient()
            {
                Name = responseResource.Name,
                Email = responseResource.Email,
                Password = responseResource.Password,
                PhoneNumber = responseResource.PhoneNumber,
                DateOfBirth = responseResource.DateOfBirth,
            };

            return patient;
        }
    }
}
