namespace DiagnosisSystem.Repositories
{
    public class QueryRepo : IQueryRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryServices _queryServices;
      

        public QueryRepo(ApplicationDbContext context, IQueryServices queryServices)
        {
            _context = context;
            _queryServices = queryServices;
        }

        public async Task <List<QueryVM>> GetSelectedPatientQueries( string userId)
        {
            var questions = await _context.Queries
                .Where(i => i.PatientId == userId)
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes = q.Votes,
                    AnswerCount = q.Answers.Where(a => a.QueryId == q.Id).Count(),
                    // QuestionTag = q.Tag != null ? q.Tag.Split(',').ToList() : new List<string>()

                }).ToListAsync();
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

        public async Task AddSpecialityToDB(Specialty specialty)
        {
            try
            {
                _context.Specialities.Add(specialty);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }

        public async Task<List<SpecialtyVM>> GetAllSpecialties()
        {
            var allSpecialties = await _context.Specialities
                .AsNoTracking()
                .Select(t => new SpecialtyVM
                {
                    Name = t.SpecialtyName,
                    Description = t.Description,

                }).ToListAsync();
            return allSpecialties;
        }

        

        //public async Task<JsonResult> AddTagToDB(Tag tag)
        //{
        //    try
        //    {
        //        _context.Tags.Add(tag);
        //        await _context.SaveChangesAsync();

        //        s
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonResult(new { success = false, message = ex.Message });
        //    }
        //}
        public async Task<List<TagVM>> GetAllTags()
        {
            var allTags = await _context.Tags
                .AsNoTracking()
                .Select(t => new TagVM
                {
                    Name = t.Name,
                    Description = t.Description,
                    SelectedSpeciality = t.SpecialityName
                }).ToListAsync();
            
            return allTags;
        }

        public async Task<List<QueryVM>> GetAllQueries()
        {
            var Queries = await _context.Queries
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes = q.Votes,
                    // QuestionTag = q.Tag.Split('-', ),
                    AnswerCount = q.Answers.Where(a => a.QueryId == q.Id).Count(),
                })
                .ToListAsync();

            return Queries;
        }
        public async Task AddSpecialityToDB(RegisterVM doctorVM)
        {
            var specialities = GetAllSpecialties().Result
                .Select(s => s.Name);

            if (!specialities.Contains(doctorVM.Speciality))
            {
                var speciality = _queryServices.ConvertToEntity(doctorVM.Speciality, string.Empty);
                await AddSpecialityToDB(speciality);
            }
        }
    }
}
