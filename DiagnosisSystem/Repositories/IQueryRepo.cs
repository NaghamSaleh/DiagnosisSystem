namespace DiagnosisSystem.Repositories
{
    public interface IQueryRepo
    {
        QueryVM GetAllAnswers(string id);
    }
}
