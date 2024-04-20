namespace DiagnosisSystem.Repositories
{
    public interface IAdminRepo
    {
        int GetAdminCount();
        string GetAdminUsername(string id);
    }
}
