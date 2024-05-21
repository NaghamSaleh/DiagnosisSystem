using DiagnosisSystem.Entities;
using DiagnosisSystem.Repositories.Interfaces;
using DiagnosisSystem.Services.Interfaces;

namespace DiagnosisSystem.Services
{
    public class PatientServices : IPatientServices
    {
        public PatientVM MapPatientModel(EditProfileVM EditProfileVM, List<QueryVM> QueryVM)
        {
            var Patient = new PatientVM();
            Patient.QueryVM = QueryVM;
            Patient.EditProfileVM = EditProfileVM;
            return Patient;
        }
    }
}
