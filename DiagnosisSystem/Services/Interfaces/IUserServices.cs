using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;

namespace DiagnosisSystem.Services.Interfaces
{
    public interface IUserServices
    {
        User CreateUserEntity(RegisterVM user);
        string GetCurrentUserName();
        string GetCurrentUserId();
    }
}
