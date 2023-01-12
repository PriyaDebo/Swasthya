using Common.Models;
using Common.DTO;
using DAL.Repositories;

namespace BL.Operations
{
    public class IndividualOperations
    {
        IndividualRepository individualRepository;

        public IndividualOperations(IndividualRepository individualRepository)
        {
            this.individualRepository = individualRepository;
        }

        public async Task<IIndividual> AddIndividualDataAsync(string email, string name, string phoneNumber, string dateOfBirth)
        {
            var individualResponse = await individualRepository.CreateIndividualAsync(email, name, phoneNumber, dateOfBirth);
            if (individualResponse != null)
            {
                return individualResponse;
            }

            return null;
        }
    }
}
