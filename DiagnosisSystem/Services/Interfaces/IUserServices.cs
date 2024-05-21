using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;

namespace DiagnosisSystem.Services.Interfaces
{
    public interface IUserServices
    {
        Task<User> MapUser(EditProfileVM model, User user);
        User CreateUserEntity(RegisterVM user);
        string GetCurrentUserName();
        string GetCurrentUserId();
    }
}
