namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IQueryRepo _queryRepo;

       
        public PatientController(ApplicationDbContext context, 
            IDoctorRepo doctorRepo, IQueryRepo queryRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _queryRepo = queryRepo;
        }
        public IActionResult Index()
        {
           return View();
        }
        
        #region Create Patient Question
        [HttpGet]
        public IActionResult Create()
        {
            var tags = _context.Tags
                .Select(s => s.Name)
                .Distinct().ToList();
            ViewBag.tags = tags;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QueryVM patientQuestionVM)
        {
            if(ModelState.IsValid)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var pQuestion = new Query()
                {
                    QueryTitle = patientQuestionVM.QueryTitle,
                    Description = patientQuestionVM.Description,
                    Tag = patientQuestionVM.QuestionTag,
                    PatientId = userId
                };

                _context.Queries.Add(pQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest("Error Saving Question");
        }
        #endregion

        #region User Question
        public IActionResult Queries()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var questions = _queryRepo.GetAllQueries(userId);
            return View(questions);
        }
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("/Edit")]
        public async Task<IActionResult> MyAccount(EditProfileVM model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Gender = model.Gender;
                    user.Telephone = model.Telephone;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home"); 
                }

                return NotFound();
            }

            return View(model); 
        }

        public IActionResult Consultants()
        {
            var doctors = _doctorRepo.GetAllDoctors();
            return View(doctors);
        }

        public IActionResult Answers(string id)
        {
            var queryDetails = _queryRepo.GetAllAnswers(id);
            return View(queryDetails);
        }

        [HttpGet]
        public IActionResult AskDoctor(string doctorId)
        {
            return View(doctorId);
        }
        
        [HttpPost]
        public async Task<IActionResult> AskDoctor(PaidConstultancy detailsViewModel)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Query query = new ()
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
            var doctors = _doctorRepo.GetAllDoctors();
            var getChosenDoctor = doctors.Where(i => i.Id == id).FirstOrDefault();
            var paidVM = new PaidConstultancy()
            {
                DoctorDTO = getChosenDoctor
            };
            var tags = _context.Tags
               .Select(s => s.Name)
               .Distinct().ToList();
            ViewBag.tags = tags;
            return View(paidVM);
        }
    }
}
