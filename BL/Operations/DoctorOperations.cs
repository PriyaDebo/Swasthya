using Common.Models;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL.Operations
{
    public class DoctorOperations
    {
        private string token;
        DoctorRepository doctorRepository;

        public DoctorOperations(string token, DoctorRepository doctorRepository)
        {
            this.token = token;
            this.doctorRepository = doctorRepository;
        }

        public async Task<IDoctor> RegisterDoctorAsync(string email, string password, string name, string phoneNumber, string registrationNumber)
        {
            var emailExists = await doctorRepository.EmailExistsAsync(email);
            if (emailExists)
            {
                return null;
            }

            var registrationNumberExists = await doctorRepository.RegistrationNumberExistsAsync(registrationNumber);
            if (registrationNumberExists)
            {
                return null;
            }

            var doctorResponse = await doctorRepository.RegisterDoctorAsync(email, password, name, registrationNumber, phoneNumber);
            if (doctorResponse != null)
            {
                return doctorResponse;
            }

            return null;
        }

        private string CreateDoctorJwtToken(IDoctor doctor)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, doctor.Name),
                new Claim(ClaimTypes.Email, doctor.Email),
                new Claim(ClaimTypes.MobilePhone, doctor.PhoneNumber),
                new Claim(ClaimTypes.SerialNumber, doctor.RegistrationNumber)
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

        public async Task<string> LoginDoctorAsync(string email, string password)
        {
            var doctorResponse = await doctorRepository.LoginDoctorAsync(email, password);
            if (doctorResponse != null)
            {
                return CreateDoctorJwtToken(doctorResponse);
            }

            return null;
        }
    }
}
