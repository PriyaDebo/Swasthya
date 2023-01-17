using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class PatientData : IPatient
    {
        public PatientData()
        {
        }

        public PatientData(PatientData individual)
        {
            Id= individual.Id;
            Email = individual.Email;
            Password = individual.Password;
            Name = individual.Name;
            PhoneNumber = individual.PhoneNumber;
            DateOfBirth = individual.DateOfBirth;

        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth")]
        public string DateOfBirth{ get; set; }
    }
}
