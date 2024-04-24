using DiagnosisSystem.Models;

namespace DiagnosisSystem.Repositories
{
    public interface IDoctorRepo
    {
        int GetDrPendingRequestsCount();
        int GetRegisteredDrCount();
        List<DoctorDTO> GetAllDoctors();
        DoctorRegisterVM GetDoctorbyId(string id);
    }
}
