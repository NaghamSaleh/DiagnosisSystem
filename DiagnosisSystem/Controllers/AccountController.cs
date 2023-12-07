using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosisSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new UsersClass
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Password = userVM.Password,
                    Telephone = userVM.Telephone,
                    DateOfBirth = userVM.DateOfBirth,
                    Gender = userVM.Gender,
                    AddedOn = DateTime.Now
                };
                 _context.Users.Add(user);
                 _context.SaveChanges();
                return View(userVM);
            }
            
            return View(userVM);
        }
    }
}
