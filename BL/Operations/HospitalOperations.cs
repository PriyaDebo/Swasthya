using BCrypt.Net;
using Common.DTO;
using Common.Models;
using DAL.Repositories;

namespace BL.Operations
{
    public class HospitalOperations
    {
        HospitalRepository hospitalRepository;
        PatientRepository patientRepository;

        public HospitalOperations(HospitalRepository hospitalRepository, PatientRepository patientRepository)
        {
            this.hospitalRepository = hospitalRepository;
            this.patientRepository = patientRepository;
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
            var hospital = await hospitalRepository.GetHospitalByEmailAsync(email);

            if (hospital == null)
            {
                return null;
            }


            if (!VerifyPasswordHash(password, hospital.Password))
            {
                return null;
            }

            return await AddPatientData(hospital);
        }

        public async Task<IHospital> GetHospitalAsync(string email)
        {
            var hospital = await hospitalRepository.GetHospitalByEmailAsync(email);
            return await AddPatientData(hospital);
        }

        public async Task<bool> AddPermittedPatientAsync(string email, string patientSwasthyaId)
        {
            var hospital = await hospitalRepository.GetHospitalByEmailAsync(email);
            if (hospital == null)
            {
                return false;
            }

            var patient = await patientRepository.GetPatientBySwasthyaIdAsync(patientSwasthyaId);
            if (patient == null)
            {
                return false;
            }

            var patientAdded = await hospitalRepository.AddPatientAsync(email, patient.Id);
            return patientAdded;
        }

        private async Task<IHospital> AddPatientData(IHospital hospitalResponse)
        {
            var hospital = new Hospital()
            {
                Name = hospitalResponse.Name,
                Email = hospitalResponse.Email,
                PhoneNumber = hospitalResponse.PhoneNumber,
                Address = hospitalResponse.Address,
                PatientIds = hospitalResponse.PatientIds,
            };

            if (hospital.PatientIds != null)
            {
                if (hospital.Patients == null)
                {
                    hospital.Patients = new List<IPatient>();
                }

                foreach (var patientId in hospital.PatientIds)
                {
                    var patient = await patientRepository.GetPatientByIdAsync(patientId);
                    if (patient != null)
                    {
                        hospital.Patients.Add(patient);
                    }
                }
            }

            return hospital;
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
