using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;

namespace DiagnosisSystem.Services
{
    public interface IUserServices
    {
        User CreateUserEntity(RegisterVM user);
    }
}
