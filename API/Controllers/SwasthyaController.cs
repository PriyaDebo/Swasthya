using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwasthyaController : ControllerBase
    {
        readonly PatientOperations patientOperations;

        public SwasthyaController(PatientOperations patientOperations)
        {
            this.patientOperations = patientOperations;
        }

        [HttpPut]
        [Route("Register/Patient")]
        public async Task<ActionResult<PatientResponseModel>> AddPatientItemAsync(PatientRequestModel request)
        {
            if (request.Email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            if (request.Password == null)
            {
                return BadRequest("Password should not be empty.");
            }

            if (request.Name == null)
            {
                return BadRequest("Name should not be empty.");
            }

            if (request.PhoneNumber == null)
            {
                return BadRequest("Phone number should not be empty.");
            }

            if (request.DateOfBirth == null)
            {
                return BadRequest("Date of birth should not be empty.");
            }

            request.DateOfBirth = Uri.UnescapeDataString(request.DateOfBirth);
            var format = "dd/MM/yyyy";
            var isDate = DateTime.TryParseExact(request.DateOfBirth, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date of Birth");
            }

            var patient = await patientOperations.AddPatientDataAsync(request.Email, request.Password, request.Name, request.PhoneNumber, parsedDate.ToString());

            if (patient == null)
            {
                return Conflict("Email Already Exists");
            }

            return Ok(patient.ToAPIModel());
        }

        [HttpPost]
        [Route("Login/Patient")]
        public async Task<ActionResult<PatientResponseModel>> LoginPatientAsync(PatientRequestModel request)
        {
            if (request.Email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            if (request.Password == null)
            {
                return BadRequest("Password should not be empty.");
            }

            var response = await patientOperations.LoginPatientAsync(request.Email, request.Password);

            if (response == null)
            {
                return BadRequest("Login Failed");
            }

            return Ok(response);
        }
    }
}