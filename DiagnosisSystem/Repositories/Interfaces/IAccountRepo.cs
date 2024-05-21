using DiagnosisSystem.Entities;

namespace DiagnosisSystem.Repositories.Interfaces
{
    public interface IAccountRepo
    {
        bool IsEmailFound(string Email);

        List<AccountDetails> GetAccountDetails(List<string> SelectedUsers);
        Task<EditProfileVM> GetAccountBasicInfo();
    }
}
