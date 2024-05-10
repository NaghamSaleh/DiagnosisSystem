namespace DiagnosisSystem.Services
{
    public interface IAuthenticationService
    {
        Task<IList<string>> SignInAsync(string email, string password);
    }
}
