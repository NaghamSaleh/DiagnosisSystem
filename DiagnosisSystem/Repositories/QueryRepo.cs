
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public class QueryRepo : IQueryRepo
    {
        private readonly ApplicationDbContext _context;

        public QueryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public QueryVM GetAllAnswers(string id)
        {
            var Id = Int16.Parse(id);

            var answers = _context.Answers.Where(a => a.QueryId == Id).Select(ans => new AnswerDTO()
            {
                AnswerBody = ans.AnswerBody,
                DoctorId = ans.DoctorId
            }).ToList();
            var queryDetails = _context.Queries
                .Where(q => q.Id.Equals(id))
                .Select(qu => new QueryVM()
                {
                    Description = qu.Description,
                    QueryTitle = qu.QueryTitle,
                    Id = qu.Id,
                    Answers = answers

                })
                .FirstOrDefault() ?? new QueryVM();
            queryDetails.Answers = answers;

            return queryDetails;
        }


    }
}
