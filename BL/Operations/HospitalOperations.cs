using BCrypt.Net;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class HospitalOperations
    {
        HospitalRepository hospitalRepository;

        public HospitalOperations(HospitalRepository hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;
        }

        public async Task<IHospital> RegisterHospital(string email, string password, string name, string address, string phoneNumber)
        {
            var emailExists = await hospitalRepository.EmailExistsAsync(email);

            if (emailExists)
            {
                return null;
            }

            password = CreatePasswordHash(password);

            var hospitalResponse = await hospitalRepository.CreateHospitalAsync(email, password, name, address, phoneNumber);
            return hospitalResponse;
        }

        public async Task<IHospital> LoginHospitalAsync(string email, string password)
        {
            var hospital = await hospitalRepository.GetHospitalAsync(email);

            if (hospital == null)
            {
                return null;
            }


            if (!VerifyPasswordHash(password, hospital.Password))
            {
                return null;
            }

            return hospital;
        }

        public async Task<IHospital> GetHospitalAsync(string email)
        {
            return await hospitalRepository.GetHospitalAsync(email);
        }

        private string CreatePasswordHash(string password)
        {
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA512);
            return password;
        }

        private bool VerifyPasswordHash(string passwordInput, string passwordOriginal)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(passwordInput, passwordOriginal, hashType: HashType.SHA512);
        }
    }
}
