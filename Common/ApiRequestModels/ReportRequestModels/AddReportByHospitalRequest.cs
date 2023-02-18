namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportByHospitalRequest
    {
        public string email { get; set; }

        public string title { get; set; }

        public Stream report { get; set; }
    }
}
