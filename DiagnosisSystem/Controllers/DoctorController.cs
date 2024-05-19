using DiagnosisSystem.Entities;
using System.Linq;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IQueryRepo _queryRepo;
        private readonly IQueryServices _queryServices;

        public DoctorController(ApplicationDbContext context, IDoctorRepo doctorRepo,
            IQueryRepo queryRepo, IQueryServices queryServices, IUserRepo userRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _queryRepo = queryRepo;
            _queryServices = queryServices;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            var doctor = User.FindFirst(ClaimTypes.Name)?.Value;
            return View();
        }


        public async Task<IActionResult> Queries(QuerySearchFilter filters)
        {
            var doctor = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            #region Get Profile Picture
            var user = _userRepo.GetProfilePicture(doctor);
            ViewData["EditProfileVM"] = user;
            #endregion
            var queries = await _queryRepo.FilterQueriesbyDoctors(doctor);
            var filteredqueries = _queryServices.FilterQueries(filters, queries);
            return View(filteredqueries);
        }


        #region View selected query

        [HttpGet]
        public IActionResult Answer(int id)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion
            var queryDetails = _queryRepo.GetAllAnswers(id);
            return View(queryDetails);
        }

        [HttpPost]
        public IActionResult Answer(AnswerDTO answer)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion
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
        public IActionResult Forums()
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var doctors = _doctorRepo.GetAllDoctors();
            return View(doctors);
        }

        public async Task<IActionResult> CreateForum(FilterVM DoctorFilters, DiscussionForumDTO forumDTO)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

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
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion
            if (ModelState.IsValid)
            {
                _context.Add(discussionForum);
                _context.SaveChanges();
                return RedirectToAction("Forum");
            }
            return View(discussionForum);
        }

        public IActionResult ViewForums()
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var allForums = _context.DiscussionForums.ToList()
                .Select(a => new DiscussionForumDTO
                {
                    Id = a.Id,
                    DiscussionTopic = a.DiscussionTopic,
                    GroupTitle = a.GroupTitle,
                    GroupAdmin = GetUserNameById(a.GroupAdmin),
                    SelectedMembers = a.SelectedMembers.Split(',').Select(GetUserNameById).ToList()
                }).ToList();

            return View(allForums);
        }



        [HttpGet]
        public async Task<IActionResult> Forum(Guid ForumId)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var forum = await _context.DiscussionForums
                .Include(A=> A.Answers)
                .Where(d => d.Id == ForumId)
                .FirstOrDefaultAsync();

            if (forum == null)
            {
                return NotFound();
            }
            if(forum.Answers == null)
            {
                forum.Answers = new List<DiscussionAnswer>();
            }

            var forumDTO = new DiscussionForumDTO
            {
                Id = forum.Id,
                GroupAdmin = GetUserNameById(forum.GroupAdmin),
                DiscussionTopic = forum.DiscussionTopic,
                GroupTitle = forum.GroupTitle,
                Answers = forum.Answers.Select(ans => new DiscussionAnswerVM
                {
                    DoctorName = GetUserNameById(ans.DoctorName),
                    AnsweredAt = ans.AnsweredAt,
                    AnswerText = ans.AnswerText
                } ?? new DiscussionAnswerVM()).ToList()
            };

            return View(forumDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Forum(Guid Id, string newAnswer)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var forum = await _context.DiscussionForums
                .Include(f => f.Answers) // Include related answers
                .FirstOrDefaultAsync(d => d.Id == Id);

            if (forum == null)
            {
                return NotFound();
            }

            var answer = new DiscussionAnswer
            {
                ForumId = forum.Id,
                DoctorName = User.FindFirst(ClaimTypes.Name)?.Value,
                AnsweredAt = DateTime.UtcNow, 
                AnswerText = newAnswer 
            };

            forum.Answers.Add(answer); 

            _context.DiscussionAnswers.Add(answer); 
            await _context.SaveChangesAsync();

            return RedirectToAction("Forums");
        }


        #endregion

   

        private string GetUserNameById(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user != null ? user.FirstName : userId;
        }
    }
}
