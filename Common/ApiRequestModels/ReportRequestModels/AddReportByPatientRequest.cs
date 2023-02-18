namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportByPatientRequest
    {
        public string title { get; set; }

        public Stream report { get; set; }
    }
}
