using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.ReportRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        //Hospital
        [HttpPut]
        [Route("addReportByHospital")]
        [Authorize(Constants.HospitalPolicy)]
        public async Task<ActionResult<ReportResponseModel>> AddReportByHospitalAsync(AddReportByHospitalRequest request)
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

        //Doctor
        [HttpGet]
        [Route("getReportsByEmailForDoctor")]
        [Authorize(Constants.DoctorPolicy)]
        public async Task<ActionResult<IEnumerable<ReportResponseModel>>> GetReportsByEmailForDoctorAsync(GetReportByEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reports = await reportOperations.GetReportsByEmailAsync(request.email);
            return Ok(reports.NamesToAPIModel());
        }

        [HttpGet]
        [Route("getReportByBlobNameForDoctor")]
        [Authorize(Constants.DoctorPolicy)]
        public async Task<ActionResult<ReportStreamResponseModel>> GetReportByBlobNameForDoctorAsync(GetReportByBlobNameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var report = await reportOperations.GetReportByBlobNameAsync(request.blobName);
            return Ok(report.ReportStreamToAPIModel());
        }

        //Patient
        [HttpPut]
        [Route("addReportByPatient")]
        [Authorize(Constants.PatientPolicy)]
        public async Task<ActionResult<ReportResponseModel>> AddReportByPatientAsync(AddReportByPatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var report = await reportOperations.AddReportAsync(User.FindFirstValue(ClaimTypes.Email), request.title, request.report);
            if (report == null)
            {
                return BadRequest("Failed to add report.");
            }

            return Ok(report.ToAPIModel());
        }

        [HttpGet]
        [Route("getReportsByEmailForPatient")]
        [Authorize(Constants.PatientPolicy)]
        public async Task<ActionResult<IEnumerable<ReportResponseModel>>> GetReportsByEmailForPatientAsync()
        {
           var reports = await reportOperations.GetReportsByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return Ok(reports.NamesToAPIModel());
        }

        [HttpGet]
        [Route("getReportByBlobNameForPatient")]
        [Authorize(Constants.PatientPolicy)]
        public async Task<ActionResult<ReportStreamResponseModel>> GetReportByBlobNameForPatientAsync(GetReportByBlobNameRequest request)
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
