using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.PatientRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Constants.PatientPolicy)]
    public class PatientController : ControllerBase
    {
        readonly PatientOperations patientOperations;

        public PatientController(PatientOperations patientOperations)
        {
            this.patientOperations = patientOperations;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<PatientResponseModel>> RegisterPatientAsync(RegisterPatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            request.DateOfBirth = Uri.UnescapeDataString(request.DateOfBirth);
            var format = "dd/MM/yyyy";
            var isDate = DateTime.TryParseExact(request.DateOfBirth, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate);
            if (!isDate)
            {
                return BadRequest("Invalid Date of Birth");
            }

            var patient = await patientOperations.CreatePatientAsync(request.Email, request.Password, request.Name, request.PhoneNumber, parsedDate.ToString());

            if (patient == null)
            {
                return Conflict("Email Already Exists");
            }

            return Ok(patient.ToAPIModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> LoginPatientAsync(LoginPatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var patient = await patientOperations.LoginPatientAsync(request.Email, request.Password);

            if (patient == null)
            {
                return BadRequest("Login Failed");
            }

            var identity = new ClaimsIdentity(Constants.PatientCookie, ClaimTypes.Email, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Email, patient.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Patient));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                Constants.PatientCookie,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(5)
                });

            return Ok();
        }

        [HttpGet]
        [Route("getPatient")]
        public async Task<ActionResult<PatientResponseModel>> GetPatientAsync()
        {
            var patient = await patientOperations.GetPatientAsync(User.FindFirstValue(ClaimTypes.Email));
            return Ok(patient.ToAPIModel());
        }
    }
}