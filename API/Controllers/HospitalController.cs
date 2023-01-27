﻿using API.Extensions;
using BL.Operations;
using Common.ApiRequestModels.HospitalRequestModels;
using Common.ApiResponseModels;
using Common.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Constants.HospitalPolicy)]
    public class HospitalController : ControllerBase
    {
        readonly HospitalOperations hospitalOperations;

        public HospitalController(HospitalOperations hospitalOperations)
        {
            this.hospitalOperations = hospitalOperations;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("register")]
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
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> LoginHospitalAsync(LoginHospitalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Email == null)
            {
                return BadRequest("Email should not be empty.");
            }

            if (request.Password == null)
            {
                return BadRequest("Password should not be empty.");
            }

            var hospital = await hospitalOperations.LoginHospitalAsync(request.Email, request.Password);

            if (hospital == null)
            {
                return BadRequest("Login Failed");
            }

            var identity = new ClaimsIdentity(Constants.HospitalCookie, ClaimTypes.Email, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Email, hospital.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Hospital));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                Constants.HospitalCookie,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(5)
                });

            return Ok();
        }
    }
}