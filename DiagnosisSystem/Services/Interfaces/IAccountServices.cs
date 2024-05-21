namespace DiagnosisSystem.Services.Interfaces
{
    public interface IAccountServices
    {
        bool ValidBirthDate(DateTime birthDate);
        Stats GetAccountsStats();
        //string GetRoleByUserEmail(string email);
        bool IsRegisterValid(RegisterVM userVM);
        EditProfileVM GetPicture();



    }
}
