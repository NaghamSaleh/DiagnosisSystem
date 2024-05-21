using DiagnosisSystem.Repositories.Interfaces;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IQueryRepo _queryRepo;


        public PatientController(ApplicationDbContext context,
            IDoctorRepo doctorRepo, IQueryRepo queryRepo, IUserRepo userRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _queryRepo = queryRepo;
            _userRepo = userRepo;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        #region Create Patient Question
        [HttpGet]
        public IActionResult Create()
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var allTags = _queryRepo.GetAllTags();
            ViewBag.tags = allTags.Result.Select(s => s.Name).Distinct().ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QueryVM patientQuestionVM)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var pQuestion = new Query()
            {
                QueryTitle = patientQuestionVM.QueryTitle,
                Description = patientQuestionVM.Description,
                Tag = patientQuestionVM.QuestionTag,
                PatientId = userId
            };

            _context.Queries.Add(pQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Queries");

        }
        #endregion


        public async Task<IActionResult> Queries()
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var questions = await _queryRepo.GetAllQueries();

            return View(questions);
        }


        public IActionResult Consultants()
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var doctors = _doctorRepo.GetAllDoctors();
            return View(doctors);
        }

        public IActionResult Answers(int id)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var queryDetails = _queryRepo.GetAllAnswers(id);
            return View(queryDetails);
        }

        [HttpGet]
        public IActionResult AskDoctor(string doctorId)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            return View(doctorId);
        }

        [HttpPost]
        public async Task<IActionResult> AskDoctor(PaidConsultancy detailsViewModel)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            Query query = new()
            {
                DoctorId = detailsViewModel.DoctorDTO.Id,
                QueryTitle = detailsViewModel.QueryVM.QueryTitle,
                Description = detailsViewModel.QueryVM.Description,
                Tag = string.Join(',', detailsViewModel.QueryVM.QuestionTag),
                PatientId = userId,
                PaidConstultant = true
            };
            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            return RedirectToAction("Queries", "Patient");
        }

        public IActionResult Details(string id)
        {
            #region Get Profile Picture
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;
            #endregion

            var doctors = _doctorRepo.GetAllDoctors();
            var getChosenDoctor = doctors.Where(i => i.Id == id).FirstOrDefault();
            var paidVM = new PaidConsultancy()
            {
                DoctorDTO = getChosenDoctor
            };
            var tags = _context.Tags
               .Select(s => s.Name)
               .Distinct().ToList();
            ViewBag.tags = tags;
            return View(paidVM);
        }


        


        [HttpPost]
        public IActionResult Upvote(int queryId)
        {
            var query = _context.Queries.Find(queryId);

            if (query == null)
            {
                return NotFound();
            }

            query.Votes++;
            _context.SaveChanges();

            return RedirectToAction("Queries");
        }
    }
}
