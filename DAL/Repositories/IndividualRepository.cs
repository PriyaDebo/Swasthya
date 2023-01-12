using Common.Models;
using DAL.Models;
using Microsoft.Azure.Cosmos;

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

        public async Task<IndividualData> CreateIndividualAsync(string email, string name, string phoneNumber, string dateOfBirth)
        {
            var individual = new IndividualData()
            {
                Email = email,
                Name = name,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

            var individualCreated = await container.CreateItemAsync<IndividualData>(individual);
            return individualCreated.Resource;
        }
    }
}
