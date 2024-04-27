namespace DiagnosisSystem.Repositories
{
    public interface IQueryRepo
    {
        QueryVM GetAllAnswers(string id);
        List<QueryVM> GetAllQueries(string userId);
    }
}
