using Common.ApiResponseModels;
using Common.Models;

namespace API.Extensions
{
    public static class ReportExtension
    {
        public static ReportResponseModel ToAPIModel(this IReport report)
        {
            var model = new ReportResponseModel
            {
                Id = report.Id,
                Title = report.Title,
                MedicalReport = report.MedicalReport,
                Email = report.Email,
            };

            return model;
        }

        public static IEnumerable<ReportResponseModel> NamesToAPIModel(this IEnumerable<IReport> reports)
        {
            var names = new List<ReportResponseModel>();

            foreach (var report in reports)
            {

                if (report != null)
                {
                    var model = new ReportResponseModel
                    {
                        Id = report.Id,
                        Title = report.Title,
                    };

                    names.Add(model);
                }
            }

            return names;
        }

        public static ReportStreamResponseModel ReportStreamToAPIModel(this Stream report)
        {
            var model = new ReportStreamResponseModel
            {
                Report = report
            };

            return model;
        }

    }
}
