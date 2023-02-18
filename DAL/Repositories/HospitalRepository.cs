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
            database = client.GetDatabase("Swasthya");
            container = database.GetContainer("Hospital");
        }

        public async Task<Boolean> EmailExistsAsync(string email)
        {
            var query = $"SELECT * FROM Hospital WHERE Hospital.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var emailResponse = container.GetItemQueryIterator<HospitalData?>(queryDefinition);
            var response = await emailResponse.ReadNextAsync();

            return response.Resource.FirstOrDefault() != null;
        }

        public async Task<HospitalData?> CreateHospitalAsync(string email, string password, string name, string address, string phoneNumber)
        {
            var hospital = new HospitalData()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                Password = password,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                PatientIds = new List<string>()
            };

            var hospitalCreated = await container.CreateItemAsync<HospitalData?>(hospital);
            return hospitalCreated.Resource;
        }

        public async Task<IHospital> GetHospitalByEmailAsync(string email)
        {
            var query = $"SELECT * FROM Hospital WHERE Hospital.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var hospitalResponse = container.GetItemQueryIterator<HospitalData?>(queryDefinition);
            var response = await hospitalResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return null;
            }

            var hospital = new HospitalData()
            {
                Id = responseResource.Id,
                Email = responseResource.Email,
                Name = responseResource.Name,
                Password = responseResource.Password,
                Address = responseResource.Address,
                PhoneNumber = responseResource.PhoneNumber,
                PatientIds = responseResource.PatientIds
            };

            return hospital;
        }

        public async Task<bool> AddPatientAsync(string email, string patientId)
        {
            var query = $"SELECT * FROM Hospital WHERE Hospital.email = @email";
            var queryDefinition = new QueryDefinition(query).WithParameter("@email", email);
            var hospitalResponse = container.GetItemQueryIterator<HospitalData?>(queryDefinition);
            var response = await hospitalResponse.ReadNextAsync();
            var responseResource = response.Resource.FirstOrDefault();

            if (responseResource == null)
            {
                return false;
            }

            var newHospital = new HospitalData(responseResource);

            newHospital.PatientIds.Add(patientId);
            await container.ReplaceItemAsync<HospitalData>(newHospital, newHospital.Id);
            return true;
        }
    }
}
