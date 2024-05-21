namespace DiagnosisSystem.Services.Interfaces
{
    public interface IPatientServices
    {
        PatientVM MapPatientModel(EditProfileVM EditProfileVM, List<QueryVM> QueryVM);
    }
}
