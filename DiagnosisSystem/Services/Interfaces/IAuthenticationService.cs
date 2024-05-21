namespace DiagnosisSystem.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IList<string>> SignInAsync(string email, string password);
    }
}
