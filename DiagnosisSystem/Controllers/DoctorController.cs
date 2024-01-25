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
            

            var queries = _context.Queries
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes= q.Votes,
                    AnswerCount= q.AnswerCount,
                })
                .ToList();
            return View(queries);
        }


        #endregion

        #region View selected query
        [HttpPost]
        //NEEDS Editing
        public IActionResult Answer(int id)
        {
            var queries = _context.Queries
                .Where(d => d.Id == id)
                .Select(q => new AnswerDTO
                {
                    Id = q.Id,
                    AnswerBody = string.Empty,
                    

                })
                .ToList();
            return View(queries);
        }
        #endregion

        #region Forum
        //create discussion group
        //view all registered doctors
        #endregion
    }
}
