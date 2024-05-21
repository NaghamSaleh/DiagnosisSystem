namespace DiagnosisSystem.Repositories.Interfaces
{
    public interface IUserRepo
    {
        
        Task<List<string>> GetAllUsers(string RoleName);
        List<RegisterVM> GetRequestDetails(List<string> SelectedUsers);
        Task CreateUser(User user, string password, string roleName);
        Task UpdateUserRole(string userId);
        Task<User> UpdateUserInfo(EditProfileVM model);
        Task DeleteUser(string userId);
        EditProfileVM GetProfilePicture(string userId);
        Task<User> GetUserbyId(string Id);
        int GetRoleCount(string RoleName);
    }

}
