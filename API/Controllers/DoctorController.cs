using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.DoctorRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Constants.DoctorPolicy)]
    public class DoctorController : ControllerBase
    {
        readonly DoctorOperations doctorOperations;


        public DoctorController(DoctorOperations doctorOperations)
        {
            this.doctorOperations = doctorOperations;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<DoctorResponseModel>> RegisterDoctorAsync(RegisterDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doctor = await doctorOperations.RegisterDoctorAsync(request.Email, request.Password, request.Name, request.PhoneNumber, request.RegistrationNumber);

            if (doctor == null)
            {
                return Conflict("Doctor Already Registered.");
            }

            return Ok(doctor.ToAPIModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> LoginDoctorAsync(LoginDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var doctor = await doctorOperations.LoginDoctorAsync(request.Email, request.Password);

            if (doctor == null)
            {
                return BadRequest("Login Failed.");
            }

            var identity = new ClaimsIdentity(Constants.DoctorCookie, ClaimTypes.Email, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Email, doctor.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Doctor));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                Constants.DoctorCookie,
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
        [Route("getDoctor")]
        public async Task<ActionResult<PatientResponseModel>> GetPatientAsync()
        {
            var doctor = await doctorOperations.GetDoctorAsync(User.FindFirstValue(ClaimTypes.Email));
            return Ok(doctor.ToAPIModel());
        }
    }
}
