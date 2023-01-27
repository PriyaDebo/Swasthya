using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class DoctorData : IDoctor
    {
        public DoctorData()
        {
        }

        public DoctorData(DoctorData doctor)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            Email = doctor.Email;
            Password = doctor.Password;
            RegistrationNumber = doctor.RegistrationNumber;
            PhoneNumber = doctor.PhoneNumber;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "swasthyaId")]
        public string SwasthyaId { get; set; }

        [JsonProperty(PropertyName = "registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }
    }
}
