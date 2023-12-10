using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //for patient
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //for patient
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Users.Any(e => e.Email == userVM.Email);

                if (checkEmail)
                {
                    return BadRequest("Email already exists");
                }
                   
                
                if (userVM.Password != userVM.ConfirmPassword)
                {
                    return BadRequest("Password doesn't match");
                }
                    
                
                var minDateOfBirth = DateTime.Today.AddYears(-100);
                var maxDateOfBirth = DateTime.Today.AddYears(-18);
                if(userVM.DateOfBirth < minDateOfBirth || userVM.DateOfBirth > maxDateOfBirth)
                {
                    return BadRequest("Invalid date of birth. Must be between 18 and 100 years old.");
                }
                    
                
               var user = new Users
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
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error saving to database");
                }
                return View(userVM);
            }

            return Ok("User created Successfully");
        }










        //for doctor: call it create
        //2 methods one HttpGet and HttpPost
        //for doctor
        [HttpGet]
        public IActionResult doctorRegister()
        {
            return View();
        }

        //for doctor
        [HttpPost]
        public async Task<IActionResult>  doctorRegister(DoctorRegisterVM MedicalPractitionerVM)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.MedicalPractitioners.Any(e => e.Email == MedicalPractitionerVM.Email);

                if (checkEmail)
                {
                    return BadRequest("Email already exists");
                }


                if (MedicalPractitionerVM.Password != MedicalPractitionerVM.ConfirmPassword)
                {
                    return BadRequest("Password doesn't match");
                }


                var minDateOfBirth = DateTime.Today.AddYears(-100);
                var maxDateOfBirth = DateTime.Today.AddYears(-18);
                if (MedicalPractitionerVM.DateOfBirth < minDateOfBirth || MedicalPractitionerVM.DateOfBirth > maxDateOfBirth)
                {
                    return BadRequest("Invalid date of birth. Must be between 18 and 100 years old.");
                }


                var doctor = new MedicalPractitioner
                {
                    MedicalPractitionerID = MedicalPractitionerVM.MedicalPractitionerID,
                    CurrentHospital = MedicalPractitionerVM.CurrentHospital,
                    Languages = MedicalPractitionerVM.Languages,
                    Specialty = MedicalPractitionerVM.Specialty,
                    Experience = MedicalPractitionerVM.Experience,
                    ShortBio = MedicalPractitionerVM.ShortBio


                };
                try
                {
                    _context.MedicalPractitioners.Add(doctor);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving to database");
                }
                return View(MedicalPractitionerVM);
            }

            return Ok("User created Successfully");
        }











        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var users = await _context.Users.Where(e => e.Email == loginVM.Email).FirstAsync();
            if (users == null || users.Password != loginVM.Password)
            {
                return BadRequest("Failed login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
