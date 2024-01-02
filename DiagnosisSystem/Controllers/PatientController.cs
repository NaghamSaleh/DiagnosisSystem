using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosisSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
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
                var pQuestion = new PatientQuestion()
                {
                    QuestionTitle = patientQuestionVM.QuestionTitle,
                    QuestionBody = patientQuestionVM.QuestionBody,
                    QuestionTag = string.Join(',', patientQuestionVM.QuestionTag)
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
            var questions = _context.PatientQuestions.Select(q => new PatientQuestionVM
            {
                Id= q.Id,
                QuestionTitle = q.QuestionTitle
            }).ToList();
            return View(questions);
        }
        #endregion
        public IActionResult ViewQueries(int id)
        {
            return View();
        }
    }
}
