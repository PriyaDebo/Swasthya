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
    }
}
