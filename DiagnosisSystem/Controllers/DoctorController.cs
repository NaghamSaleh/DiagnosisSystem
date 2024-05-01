namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IQueryRepo _queryRepo;
        private readonly IQueryServices _queryServices;

        public DoctorController(ApplicationDbContext context, IDoctorRepo doctorRepo,
            IQueryRepo queryRepo, IQueryServices queryServices)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _queryRepo = queryRepo;
            _queryServices = queryServices;
        }

        public IActionResult Index()
        {
            var doctor = User.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }


        public async Task<IActionResult> Queries(QuerySearchFilter filters)
        {
            var queries = await _queryRepo.GetAllQueries();
            var filteredqueries = _queryServices.FilterQueries(filters, queries);
            return View(filteredqueries);
        }


        #region View selected query

        [HttpGet]
        public IActionResult Answer(int id)
        {
            
            var queryDetails = _queryRepo.GetAllAnswers(id);
            return View(queryDetails);
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
        public IActionResult Forum()
        {
            var doctors = _doctorRepo.GetAllDoctors();
            return View(doctors);
        }

        public async Task<IActionResult> CreateForum(FilterVM DoctorFilters, DiscussionForumDTO forumDTO)
        {
            var speciality = await _context.Specialities.Select(s => new SpecialtyVM()
            {
                Name = s.SpecialtyName,
                Id = s.SpecialtyID
            }).ToListAsync();

            var hospitals = _context.Users
                .Where(a => a.CurrentHospital != null)
                .Select(h =>
                h.CurrentHospital)
                .Distinct()
                .ToList();

            var alllanguages = _context.Users
                .Where(l => l.Languages != null)
                .Distinct()
                .Select(l => l.Languages).ToList();

            var dExperiences = _context.Users
                .Select(e => e.Experience)
                .Distinct().ToList();

            ViewBag.speciality = speciality;
            ViewBag.specialityinput = DoctorFilters.SpecilityName;
            ViewBag.hospitals = hospitals;
            ViewBag.alllanguages = alllanguages;
            ViewBag.dExperiences = dExperiences;

            var doctors = _doctorRepo.GetAllDoctors();


            if (DoctorFilters.CurrentHospital is not null && DoctorFilters.CurrentHospital != "All")
            {
                doctors = doctors.Where(c => c.CurrentHospital == DoctorFilters.CurrentHospital).ToList();
            }
            if (DoctorFilters.Experience > 0)
            {
                doctors = doctors.Where(e => e.Experience == DoctorFilters.Experience).ToList();
            }
            if (DoctorFilters.Languages is not null && DoctorFilters.Languages != "All")
            {
                doctors = doctors.Where(e => e.Languages == DoctorFilters.Languages).ToList();
            }
            if (DoctorFilters.SpecilityName is not null && DoctorFilters.SpecilityName != "All")
            {
                doctors = doctors.Where(e => e.Speciality == DoctorFilters.SpecilityName).ToList();
            }

            var filterResult = new FilterVM()
            {
                Doctors = doctors
            };
            DiscussionForumTable forumTable = new DiscussionForumTable
            {
                DoctorFilters = filterResult,
                ForumDTO = forumDTO
            };

            if (forumDTO.GroupTitle is not null)
            {
                var forumEntity = new DiscussionForum
                {
                    DiscussionTopic = forumDTO.DiscussionTopic,
                    GroupTitle = forumDTO.GroupTitle,
                    GroupAdmin = forumDTO.GroupAdmin,
                    SelectedMembers = string.Join(',', forumDTO.SelectedMembers),
                    

                };
                _context.DiscussionForums.Add(forumEntity);
                _context.SaveChanges();
            }


            return View(forumTable);
        }

   
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
