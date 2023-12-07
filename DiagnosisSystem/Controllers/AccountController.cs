using DiagnosisSystem.Data;
using DiagnosisSystem.entities;
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

        public IActionResult Register(UserVM userVM)
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
            return View();
        }
    }
}
