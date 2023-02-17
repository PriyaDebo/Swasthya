namespace Common.ApiRequestModels.ReportRequestModels
{
    public class AddReportRequestModel
    {
        public string email { get; set; }

        public string title { get; set; }

        public Stream report { get; set; }
    }
}
