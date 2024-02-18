using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using DiagnosisSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDoctorRepo _doctorRepo;

       
        public PatientController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IDoctorRepo doctorRepo)
        {
            _context = context;
            _userManager = userManager;
            _doctorRepo = doctorRepo;
        }
        public IActionResult Index()
        {
           return View();
        }
        
        #region Create Patient Question
        [HttpGet]
        public IActionResult Create()
        {
            var questionVM = new QueryVM();
            questionVM.QuestionTag = _context.Tags.Select(s => s.Name).ToList();

            return View(questionVM);
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
                    Tag = string.Join(',', patientQuestionVM.QuestionTag),
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
            var questions = _context.Queries
                .Where(i => i.PatientId == userId)
                .Select(q => new QueryVM
            {
                Id= q.Id,
                QueryTitle = q.QueryTitle,
                Votes= q.Votes,
                   // QuestionTag = q.Tag != null ? q.Tag.Split(',').ToList() : new List<string>()

                }).ToList();
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

        public IActionResult Details(string id)
        {
            var doctor = _doctorRepo.GetDoctorbyId(id);
            return View(doctor);
        }
    }
}
