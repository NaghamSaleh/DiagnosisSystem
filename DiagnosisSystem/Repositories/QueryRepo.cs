
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiagnosisSystem.Repositories
{
    public class QueryRepo : IQueryRepo
    {
        private readonly ApplicationDbContext _context;

        public QueryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<QueryVM> GetAllQueries( string userId)
        {
            var questions = _context.Queries
                .Where(i => i.PatientId == userId)
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes = q.Votes,
                    AnswerCount = q.Answers.Where(a => a.QueryId == q.Id).Count(),
                    // QuestionTag = q.Tag != null ? q.Tag.Split(',').ToList() : new List<string>()

                }).ToList();
            return questions;
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
