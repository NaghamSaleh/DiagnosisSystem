namespace DiagnosisSystem.Repositories.Interfaces
{
    public interface IAdminRepo
    {
        int GetAdminCount();
        string GetAdminUsername(string id);
    }

}
