using Common.ApiResponseModels.ReportResponseModels;
using Common.Models;
using System.IO;
using System.Text;

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
                        MedicalReport = report.MedicalReport,
                        Title = report.Title,
                    };

                    names.Add(model);
                }
            }

            return names;
        }

        public static ReportStreamResponseModel ReportStreamToAPIModel(this Stream report)
        {
            report.Position = 0;
            var memoryStream = new MemoryStream();
            memoryStream.Position = 0;
            report.CopyTo(memoryStream);
            var model = new ReportStreamResponseModel
            {

                Report = Encoding.ASCII.GetString(memoryStream.ToArray())
            };

            return model;
        }

    }
}
