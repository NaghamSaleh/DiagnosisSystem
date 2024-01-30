using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
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
        
        public IActionResult Queries(QuerySearchFilter filters)
        {
            var queries = _context.Queries
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Votes= q.Votes,
                    
                    AnswerCount = q.Answers.Where(a=>a.QueryId == q.Id).Count(),
                })
                .ToList();
           
            if(filters is not null && filters.Answered is false)
            {
                queries = queries
                    .OrderByDescending(q => q.AnswerCount == 0 ? int.MaxValue : q.AnswerCount)
                    .ToList();
            }
            if (filters is not null && filters.Answered is false)
            {
                queries = queries
                    .OrderByDescending(q => q.AnswerCount >= 0 ? int.MaxValue : q.AnswerCount)
                    .ToList();
            }
            var filteredqueries = new QueryTableVM()
            {
                Queries = queries
            };
            return View(filteredqueries);
        }


        #endregion

        #region View selected query

        [HttpGet]        
        public IActionResult Answer(int id)
        {
            var queries = _context.Queries
                .Where(d => d.Id == id)
                .Select(q => new QueryVM
                {
                    Id = q.Id,
                    QueryTitle = q.QueryTitle,
                    Description = q.Description,
                   
                }).FirstOrDefault();
            return View(queries);
        }

        [HttpPost]
        public IActionResult Answer(AnswerDTO answer)
        {
            var ans = new Answer()
            {
                AnswerBody = answer.AnswerBody,
                DoctorId = answer.DoctorId,
                QueryId = answer.QueryId,
            };

            return Ok("Successfully Answered");
        }
        #endregion

        #region Forum
        //create discussion group
        //view all registered doctors
        #endregion
    }
}
