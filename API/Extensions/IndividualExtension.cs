using Common.ApiModels;
using Common.Models;

namespace API.Extensions
{
    public static class IndividualExtension
    {
        public static IndividualResponseModel ToAPIModel(this IIndividual iIndividual)
        {
            var model = new IndividualResponseModel
            {
                Email = iIndividual.Email,
                Name = iIndividual.Name,
                PhoneNumber = iIndividual.PhoneNumber,
                DateOfBirth = iIndividual.DateOfBirth,
            };

            return model;
        }
    }
}
