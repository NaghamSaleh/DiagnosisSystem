using DiagnosisSystem.Models;

namespace DiagnosisSystem.Repositories
{
    public interface IDoctorRepo
    {
        //int GetDrPendingRequestsCount();
        //int GetRegisteredDrCount();
        List<DoctorDTO> GetAllDoctors();
        RegisterVM GetDoctorbyId(string id);
        //int GetRoleCount(string RoleName);
    }
}
