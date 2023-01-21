using Common.Models;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL.Operations
{
    public class PatientOperations
    {
        private string token;
        PatientRepository patientRepository;

        public PatientOperations(string token, PatientRepository patientRepository)
        {
            this.token = token;
            this.patientRepository = patientRepository;
        }

        public async Task<IPatient> AddPatientDataAsync(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            var emailExists = await patientRepository.EmailExistsAsync(email);

            if (emailExists)
            {
                return null;
            }

            var patientResponse = await patientRepository.CreatePatientAsync(email, password, name, phoneNumber, dateOfBirth);
            if (patientResponse != null)
            {
                return patientResponse;
            }

            return null;
        }

        private string CreatePatientJwtToken(IPatient patient)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, patient.Name),
                new Claim(ClaimTypes.Email, patient.Email),
                new Claim(ClaimTypes.DateOfBirth, patient.DateOfBirth),
                new Claim(ClaimTypes.MobilePhone, patient.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(token));

            var loginCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: loginCredentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return jwt;
        }

        public async Task<string> LoginPatientAsync(string email, string password)
        {
            var patientResponse = await patientRepository.LoginPatientAsync(email, password);

            if (patientResponse != null)
            {
                return CreatePatientJwtToken(patientResponse);
            }

            return null;
        }
    }
}
