using Common.Models;

namespace Common.DTO
{
    public class Report : IReport
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string MedicalReport { get; set; }
    }
}
