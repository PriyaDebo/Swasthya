using BCrypt.Net;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class PatientOperations
    {
        PatientRepository patientRepository;

        public PatientOperations(PatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<IPatient> CreatePatientAsync(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            var emailExists = await patientRepository.EmailExistsAsync(email);

            if (emailExists)
            {
                return null;
            }

            password = CreatePasswordHash(password);

            Random random = new Random();
            var swasthyaId = name + "_" + random.Next();

            var patientResponse = await patientRepository.CreatePatientAsync(email, password, name, swasthyaId, phoneNumber, dateOfBirth);
            return patientResponse;
        }

        public async Task<IPatient> LoginPatientAsync(string email, string password)
        {
            var patient = await patientRepository.GetPatientAsync(email);

            if (patient == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, patient.Password))
            {
                return null;
            }

            return patient;

        }

        public async Task<IPatient> GetPatientAsync(string email)
        {
            return await patientRepository.GetPatientAsync(email);
        }

        public async Task<bool> AddPermittedDoctorIdAsync(string email, string doctorSwasthyaId)
        {
            return await patientRepository.AddPermittedDoctorIdAsync(email, doctorSwasthyaId);
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
