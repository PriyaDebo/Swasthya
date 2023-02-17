using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.ReportRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        readonly ReportOperations reportOperations;

        public ReportController(ReportOperations reportOperations)
        {
            this.reportOperations = reportOperations;
        }

        [HttpPut]
        [Route("addReport")]
        [Authorize(Constants.HospitalPolicy)]
        public async Task<ActionResult<ReportResponseModel>> AddReportAsync(AddReportRequestModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var report = await reportOperations.AddReportAsync(request.email, request.title, request.report);
            if (report == null)
            {
                return BadRequest("Failed to add report.");
            }

            return Ok(report.ToAPIModel());
        }

        [HttpGet]
        [Route("getReportsByEmail")]
        [Authorize(Constants.PatientPolicy + "," + Constants.PatientPolicy)]
        public async Task<ActionResult<IEnumerable<ReportResponseModel>>> GetResportsByEmailAsync(GetReportByEmailRequestModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reports = await reportOperations.GetReportsByEmailAsync(request.email);
            return Ok(reports.NamesToAPIModel());
        }

        [HttpGet]
        [Route("getReportByBlobName")]
        [Authorize(Constants.PatientPolicy + "," + Constants.PatientPolicy)]
        public async Task<ActionResult<ReportStreamResponseModel>> GetReportByBlobNameAsync(GetReportByBlobNameRequestModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var report = await reportOperations.GetReportByBlobNameAsync(request.blobName);
            return Ok(report.ReportStreamToAPIModel());
        }

    }
}
