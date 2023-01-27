using BCrypt.Net;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class DoctorOperations
    {
        DoctorRepository doctorRepository;

        public DoctorOperations(DoctorRepository doctorRepository)
        {
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

            password = CreatePasswordHash(password);

            Random random = new Random();
            var swasthyaId = name + "_" + random.Next();

            var doctorResponse = await doctorRepository.CreateDoctorAsync(email, password, name, swasthyaId, registrationNumber, phoneNumber);
            return doctorResponse;
        }

        public async Task<IDoctor> LoginDoctorAsync(string email, string password)
        {
            var doctor = await doctorRepository.GetDoctorAsync(email);

            if (doctor == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, doctor.Password))
            {
                return null;
            }

            return doctor;
        }

        public async Task<IDoctor> GetDoctorAsync(string email)
        {
            return await doctorRepository.GetDoctorAsync(email);
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
