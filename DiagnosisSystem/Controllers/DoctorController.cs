namespace DiagnosisSystem.Controllers
{
   // [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;

        public DoctorController(ApplicationDbContext context, IDoctorRepo doctorRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
        }

        public IActionResult Index()
        {
            var doctor = User.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }

        #region All Question

        public IActionResult Queries(QuerySearchFilter filters)
        {
            var queries = _context.Queries
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes = q.Votes,
                    // QuestionTag = q.Tag.Split('-', ),
                    AnswerCount = q.Answers.Where(a => a.QueryId == q.Id).Count(),
                })
                .ToList();

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
            var filteredqueries = new QueryTableVM()
            {
                Queries = queries
            };
            return View(filteredqueries);
        }


        #endregion

        #region View selected query

        [HttpGet]
        public IActionResult Answer(int id)
        {
            var queries = _context.Queries
                .Where(d => d.Id == id)
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Description = q.Description,

                }).FirstOrDefault();
            var answers =
                     new AnswerDTO
                     {
                         Query = queries,

                     };
            return View(answers);
        }

        [HttpPost]
        public IActionResult Answer(AnswerDTO answer)
        {
            var ans = new Answer()
            {
                AnswerBody = answer.AnswerBody,
                DoctorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                QueryId = answer.QueryId,
            };
            _context.Answers.Add(ans);
            _context.SaveChanges();
            return Ok("Successfully Answered");
        }
        #endregion

        #region Forum
        public async Task<IActionResult> Forum(FilterVM filter)
        {
            //Select * from Specialities
            var speciality = await _context.Specialities.Select(s => new SpecialityVM()
            {
                Name = s.SpecialtyName,
                Id = s.SpecialtyID
            }).ToListAsync();

            var hospitals = _context.Users
                .Where(a => a.CurrentHospital != null)
                .Select(h => 
                h.CurrentHospital)
                .ToList();

            var alllanguages = _context.Users.Where(l=> l.Languages != null)
                .Select(l => l.Languages).ToList();

            var dExperiences = _context.Users
                .Select(e => e.Experience).ToList();

            ViewBag.speciality = speciality;
            ViewBag.specialityinput = filter.SpecilityName;
            ViewBag.hospitals = hospitals;
            ViewBag.alllanguages = alllanguages;
            ViewBag.dExperiences = dExperiences;
            
            var doctors = _doctorRepo.GetAllDoctors();


            if(filter.CurrentHospital is not null)
            {
                doctors = doctors.Where(c => c.CurrentHospital == filter.CurrentHospital).ToList();
            }
            if(filter.Experience > 0)
            {
                doctors = doctors.Where(e=>e.Experience == filter.Experience).ToList();
            }
            if(filter.Languages != "0" && filter.Languages is not null)
            {
                doctors = doctors.Where(e => e.Languages == filter.Languages).ToList();
            }
            if (filter.SpecilityName is not null)
            {
                doctors = doctors.Where(e => e.Speciality == filter.SpecilityName).ToList();
            }
            FilterVM reportResult = new();

            reportResult.Doctors = doctors;
            return View(reportResult);
        }

        //[HttpGet]
        //public IActionResult CreateForum()
        //{
        //    var doctors = _doctorRepo.GetAllDoctors();
        //    var forumVM = new DiscussionForumDTO
        //    {
        //        AllMembers = doctors
        //    };
        //    return View(forumVM);
        //}

        [HttpPost]
        public IActionResult Create(DiscussionForumDTO discussionForum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discussionForum);
                _context.SaveChanges();
                return RedirectToAction("Forum");
            }
            return View(discussionForum);
        }

        //[HttpPost]
        //public IActionResult AddMember(string memberName)
        //{
        //    // Assuming you have a way to identify the discussion group currently being viewed
        //    var discussionGroup = _context.DiscussionGroups
        //        .Include(g => g.Members)
        //        .FirstOrDefault(); // Change this query as needed based on how you identify the group
        //    discussionGroup.AddMember(memberName);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        #endregion

        [HttpGet]
        public IActionResult MyAccount()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users
                .Where(i => i.Id == userId)
                .Select(u => new EditProfileVM()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Gender = u.Gender,
                    Telephone = u.Telephone
                }).FirstOrDefault();
            return View(user);
        }
    }
}
