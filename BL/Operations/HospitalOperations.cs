using Common.Models;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL.Operations
{
    public class HospitalOperations
    {
        private string token;
        HospitalRepository hospitalRepository;

        public HospitalOperations(string token, HospitalRepository hospitalRepository)
        {
            this.token = token;
            this.hospitalRepository = hospitalRepository;
        }

        public async Task<IHospital> RegisterHospital(string email, string password, string name, string address, string phoneNumber)
        {
            var emailExists = await hospitalRepository.EmailExistsAsync(email);

            if (emailExists)
            {
                return null;
            }

            var hospitalResponse = await hospitalRepository.RegisterHospitalAsync(email, password, name, address, phoneNumber);
            return hospitalResponse;
        }

        private string CreateHospitalJwtToken(IHospital hospital)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, hospital.Name),
                new Claim(ClaimTypes.Email, hospital.Email),
                new Claim(ClaimTypes.StreetAddress, hospital.Address),
                new Claim(ClaimTypes.MobilePhone, hospital.PhoneNumber)
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

        public async Task<string> LoginHospitalAsync(string email, string password)
        {
            var hospitalResponse = await hospitalRepository.LoginHospitalAsync(email, password);

            if (hospitalResponse != null)
            {
                return CreateHospitalJwtToken(hospitalResponse);
            }

            return null;
        }
    }
}
