using DiagnosisSystem.Data;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Doctor")]
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
                .Select(query => new QueryVM
                {
                    Id = query.Id,
                    QueryTitle = query.QueryTitle,
                    Votes= query.Votes,
                    AnswerCount= query.AnswerCount,
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
