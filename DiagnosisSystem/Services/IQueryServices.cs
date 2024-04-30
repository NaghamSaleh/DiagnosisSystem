namespace DiagnosisSystem.Services
{
    public interface IQueryServices
    {
        Specialty ConvertToEntity(string Name, string Description);
        Tag ConvertToEntity(TagVM tagVM);
        QueryTableVM FilterQueries(QuerySearchFilter filters, List<QueryVM> queries);
    }
}
