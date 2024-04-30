namespace DiagnosisSystem.Repositories
{
    public interface IUserRepo
    {
        int GetRoleCount(string RoleName);
        Task<List<string>> GetAllUsers(string RoleName);
        List<AccountDetails> GetAccountDetails(List<string> SelectedUsers);
        bool IsEmailFound(string Email);
    }
}
