using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwasthyaController : ControllerBase
    {
        PatientOperations individualOperations;

        public SwasthyaController(PatientOperations patientOperations)
        {
            this.individualOperations = patientOperations;
        }

        [HttpPut]
        [Route("RegisterPatient")]
        public async Task<ActionResult<PatientResponseModel>> AddPatientItemAsync(PatientRequestModel request)
        {
            //string format = "dd/MM/yyyy";

            //var isDate = DateTime.TryParseExact(dateOfBirth, "format", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate);
            //if (!isDate)
            //{
            //    return BadRequest("Invalid Date of Birth");
            //}

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

            var patient = await individualOperations.AddPatientDataAsync(request.Email, request.Password, request.Name, request.PhoneNumber, request.DateOfBirth);

            if (patient == null)
            {
                return Conflict("Email Already Exists");
            }

            return Ok(patient.ToAPIModel());
        }

        [HttpGet]
        [Route("GetPatient/email/{email}")]
        public async Task<ActionResult<PatientResponseModel>> GetPatientByEmailAsync(string email)
        {
            if (email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            return Ok();
        }
    }
}