using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.DoctorRequestModels;
using Common.ApiRequestModels.HospitalRequestModels;
using Common.ApiRequestModels.PatientRequestModels;
using Common.ApiResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Runtime.InteropServices;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwasthyaController : ControllerBase
    {
        readonly PatientOperations patientOperations;
        readonly DoctorOperations doctorOperations;
        readonly HospitalOperations hospitalOperations;

        public SwasthyaController(PatientOperations patientOperations, DoctorOperations doctorOperations, HospitalOperations hospitalOperations)
        {
            this.patientOperations = patientOperations;
            this.doctorOperations = doctorOperations;
            this.hospitalOperations = hospitalOperations;
        }

        [HttpPut]
        [Route("Register/Patient")]
        public async Task<ActionResult<PatientResponseModel>> RegisterPatientAsync(RegisterPatientRequest request)
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

            var patient = await patientOperations.RegisterPatientAsync(request.Email, request.Password, request.Name, request.PhoneNumber, parsedDate.ToString());

            if (patient == null)
            {
                return Conflict("Email Already Exists");
            }

            return Ok(patient.ToAPIModel());
        }

        [HttpPost]
        [Route("Login/Patient")]
        public async Task<ActionResult<string>> LoginPatientAsync(LoginPatientRequest request)
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

        [HttpPut]
        [Route("Register/Doctor")]
        public async Task<ActionResult<DoctorResponseModel>> RegisterDoctorAsync(RegisterDoctorRequest request)
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

            if (request.RegistrationNumber == null)
            {
                return BadRequest("Registration number should not be empty.");
            }

            var doctor = await doctorOperations.RegisterDoctorAsync(request.Email, request.Password, request.Name, request.PhoneNumber, request.RegistrationNumber);

            if (doctor == null)
            {
                return Conflict("Doctor Already Registered.");
            }

            return Ok(doctor.ToAPIModel());
        }

        [HttpPost]
        [Route("Login/Doctor")]
        public async Task<ActionResult<string>> LoginDoctorAsync(LoginDoctorRequest request)
        {
            if (request.Email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            if (request.Password == null)
            {
                return BadRequest("Password should not be empty.");
            }

            var response = await doctorOperations.LoginDoctorAsync(request.Email, request.Password);

            if (response == null)
            {
                return BadRequest("Login Failed.");
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("Register/Hospital")]
        public async Task<ActionResult<HospitalResponseModel>> RegisterHospitalAsync(RegisterHospitalRequest request)
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

            if (request.Address == null)
            {
                return BadRequest("Address should not be empty.");
            }

            var hospital = await hospitalOperations.RegisterHospital(request.Email, request.Password, request.Name, request.Address, request.PhoneNumber);

            if (hospital == null)
            {
                return BadRequest("Email already registered.");
            }

            return Ok(hospital.ToAPIModel());
        }

        [HttpPost]
        [Route("Login/Hospital")]
        public async Task<ActionResult<string>> LoginHospitalAsync(LoginHospitalRequest request)
        {
            if (request.Email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            if (request.Password == null)
            {
                return BadRequest("Password should not be empty.");
            }

            var response = await hospitalOperations.LoginHospitalAsync(request.Email, request.Password);

            if (response == null)
            {
                return BadRequest("Login Failed");
            }

            return Ok(response);
        }
    }
}