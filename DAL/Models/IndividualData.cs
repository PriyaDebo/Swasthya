using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class IndividualData : IIndividual
    {
        public IndividualData()
        {
        }

        public IndividualData(IndividualData individual)
        {
            Email = individual.Email;
            Name = individual.Name;
            PhoneNumber = individual.PhoneNumber;
            DateOfBirth = individual.DateOfBirth;
        }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth")]
        public string DateOfBirth{ get; set; }
    }
}
