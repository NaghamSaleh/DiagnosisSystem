namespace DiagnosisSystem.Repositories.Interfaces
{
    public interface IUserRepo
    {
        int GetRoleCount(string RoleName);
        Task<List<string>> GetAllUsers(string RoleName);
        List<AccountDetails> GetAccountDetails(List<string> SelectedUsers);
        List<RegisterVM> GetRequestDetails(List<string> SelectedUsers);
        bool IsEmailFound(string Email);
        Task CreateUser(User user, string password, string roleName);
        Task UpdateUserRole(string userId);
        Task DeleteUser(string userId);
        EditProfileVM GetProfilePicture(string userId);
    }

}
