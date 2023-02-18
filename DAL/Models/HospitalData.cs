using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DAL.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class HospitalData : IHospital
    {
        public HospitalData()
        {
        }

        public HospitalData(HospitalData hospital)
        {
            Id = hospital.Id;
            Email = hospital.Email;
            Password = hospital.Password;
            Name = hospital.Name;
            Address = hospital.Address;
            PhoneNumber = hospital.PhoneNumber;
            PatientIds = hospital.PatientIds;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "patientIds")]
        public List<string> PatientIds { get; set;}

        [JsonIgnore]
        public List<IPatient> Patients { get; set;}
    }
}
