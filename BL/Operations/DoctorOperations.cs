using BCrypt.Net;
using Common.DTO;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class DoctorOperations
    {
        DoctorRepository doctorRepository;
        PatientRepository patientRepository;

        public DoctorOperations(DoctorRepository doctorRepository, PatientRepository patientRepository)
        {
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
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
            var doctor = await doctorRepository.GetDoctorByEmailAsync(email);

            if (doctor == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, doctor.Password))
            {
                return null;
            }

            return await AddPatientData(doctor);
        }

        public async Task<IDoctor> GetDoctorAsync(string email)
        {
            var doctor = await doctorRepository.GetDoctorByEmailAsync(email);
            if (doctor != null)
            {
                return await AddPatientData(doctor); ;
            }

            return null;
        }

        private async Task<IDoctor> AddPatientData(IDoctor doctorResponse)
        {
            var doctor = new Doctor()
            {
                Id = doctorResponse.Id,
                Name = doctorResponse.Name,
                Email = doctorResponse.Email,
                SwasthyaId = doctorResponse.SwasthyaId,
                PhoneNumber = doctorResponse.PhoneNumber,
                RegistrationNumber = doctorResponse.RegistrationNumber,
                PatientIds = doctorResponse.PatientIds
            };

            if (doctor.PatientIds != null)
            {
                if (doctor.Patients == null)
                {
                    doctor.Patients = new List<IPatient>();
                }

                foreach (var patientId in doctor.PatientIds)
                {
                    var patient = await patientRepository.GetPatientByIdAsync(patientId);
                    if (patient != null)
                    {
                        doctor.Patients.Add(patient);
                    }
                }
            }

            return doctor;
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
