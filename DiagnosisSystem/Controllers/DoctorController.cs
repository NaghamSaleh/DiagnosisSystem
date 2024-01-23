using DiagnosisSystem.Data;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiagnosisSystem.Controllers
{
    public class DoctorController: Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctor = User.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }

        #region All Question
        [HttpGet]
        public IActionResult Queries()
        {
            var questions = _context.PatientQuestions
                .Select(q => new PatientQuestionVM
                {
                    Id = q.Id,
                    QuestionTitle = q.QuestionTitle,
                })
                .ToList();
            return View(questions);
        }

        [HttpPost]
        //SELECT QUERY
        public IActionResult Queries(int id)
        {
            var questions = _context.PatientQuestions
                .Where(d => d.Id == id)
                .Select(q => new PatientQuestionVM
                {
                    Id = q.Id,
                    QuestionTitle = q.QuestionTitle,

                })
                .ToList();
            return View(questions);
        }
        #endregion

        #region View selected query
        //GET AND POST
        #endregion

        #region Forum
        #endregion
    }
}
