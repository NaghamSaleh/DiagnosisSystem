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

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(PatientQuestionVM patientQuestionVM)
        {
            if(ModelState.IsValid)
            {
                var pQuestion = new PatientQuestion()
                {
                    QuestionTitle = patientQuestionVM.QuestionTitle,
                    QuestionBody = patientQuestionVM.QuestionBody,
                    //QuestionTag = patientQuestionVM.QuestionTag
                };
                _context.PatientQuestions.Add(pQuestion);
                await _context.SaveChangesAsync();
                return View();
            }
            return BadRequest("Error Saving Question");
        }
    }
}
