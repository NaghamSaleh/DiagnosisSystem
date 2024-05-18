namespace DiagnosisSystem.Services
{
    public class QueryServices : IQueryServices
    {
        public Specialty ConvertToEntity(string Name, string Description)
        {

            var speciality = new Specialty()
            {
                SpecialtyName = Name,
                Description = Description
            };
            return speciality;
        }
        public Tag ConvertToEntity(TagVM tagVM)
        {
            var tag = new Tag()
            {
                Name = tagVM.Name,
                Description = tagVM.Description,
                SpecialityName = tagVM.SelectedSpeciality
            };
            return tag;
        }
        public QueryTableVM FilterQueries(QuerySearchFilter filters, List<QueryVM> queries)
        {
            if (filters is not null && filters.Answered is false)
            {
                queries = queries
                    .OrderByDescending(q => q.AnswerCount == 0 ? int.MaxValue : q.AnswerCount)
                    .ToList();
            }
            if (filters is not null && filters.Answered is false)
            {
                queries = queries
                    .OrderByDescending(q => q.AnswerCount >= 0 ? int.MaxValue : q.AnswerCount)
                    .ToList();
            }
            queries= queries
                .OrderByDescending(q => q.ConsuntacyType == "Paid")
                .ThenBy(q => q.ConsuntacyType)
                .ToList();
            var filteredqueries = new QueryTableVM()
            {
                Queries = queries
            };
            return filteredqueries;
        }

    }
}
