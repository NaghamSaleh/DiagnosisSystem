namespace DiagnosisSystem.Repositories
{
    public interface IDoctorRepo
    {
        int GetDrPendingRequestsCount();
        int GetRegisteredDrCount();
    }
}
