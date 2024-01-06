using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiagnosisSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

       
        public PatientController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
           return View();
        }
        
        #region Create Patient Question
        [HttpGet]
        public IActionResult Create()
        {
            var questionVM = new PatientQuestionVM();
            questionVM.QuestionTag = _context.Tags.Select(s => s.Name).ToList();

            return View(questionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientQuestionVM patientQuestionVM)
        {
            if(ModelState.IsValid)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var pQuestion = new PatientQuestion()
                {
                    QuestionTitle = patientQuestionVM.QuestionTitle,
                    QuestionBody = patientQuestionVM.QuestionBody,
                    QuestionTag = string.Join(',', patientQuestionVM.QuestionTag),
                    PatientId = userId
                };

                _context.PatientQuestions.Add(pQuestion);
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
            var questions = _context.PatientQuestions.Where(i => i.PatientId == userId).Select(q => new PatientQuestionVM
            {
                Id= q.Id,
                QuestionTitle = q.QuestionTitle,
                
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
        public async Task<IActionResult> EditProfile(EditProfileVM model)
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

                    return RedirectToAction("Index", "Home"); // Redirect to home or profile page after successful update
                }

                return NotFound(); // Handle the case where the user isn't found
            }

            return View(model); // Return to the form with validation errors
        }

    }
}
