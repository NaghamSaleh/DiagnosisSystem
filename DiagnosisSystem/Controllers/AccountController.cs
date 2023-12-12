using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosisSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Patient Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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

           
               var user = new User
               {
                   FirstName = userVM.FirstName,
                   LastName = userVM.LastName,
                   Email = userVM.Email,
                 
                   Telephone = userVM.Telephone,
                   DateOfBirth = userVM.DateOfBirth,
                   Gender = userVM.Gender,
                   AddedOn = DateTime.Now,
                   Role = "Patient"
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
                //var result = await _userManager.CreateAsync(user, userVM.Password);
                //if (result.Succeeded)
                //{
                //    _context.SaveChanges();
                //    return View(null);
                //}
                return View();
            }

            return BadRequest("Rety Again please");
        }
        #endregion

        #region Doctor Register 
        [HttpGet]
        public IActionResult doctorRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> doctorRegister(DoctorRegisterVM MedicalPractitionerVM)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Users.Any(e => e.Email == MedicalPractitionerVM.Email);

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
                    Email = MedicalPractitionerVM.Email,
                    FirstName = MedicalPractitionerVM.FirstName,
                    LastName = MedicalPractitionerVM.LastName,
                    DateOfBirth = MedicalPractitionerVM.DateOfBirth,
                    Gender= MedicalPractitionerVM.Gender,
                    Telephone = MedicalPractitionerVM.Telephone,
                    CurrentHospital = MedicalPractitionerVM.CurrentHospital,
                    Languages = MedicalPractitionerVM.Languages,
                    Specialty = MedicalPractitionerVM.Specialty,
                    Experience = MedicalPractitionerVM.Experience,
                    ShortBio = MedicalPractitionerVM.ShortBio,
                   
                    AddedOn = DateTime.Now,
                    
                    Role = "Doctor"

                };
                
                try
                {
                    _context.Users.Add(doctor);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving to database");
                }
                //var result = await _userManager.CreateAsync(doctor, MedicalPractitionerVM.Password);
                //if (result.Succeeded)
                //{
                //    _context.SaveChanges();
                //    return View(MedicalPractitionerVM);
                //}
                //return BadRequest("Saing error");
                return View(MedicalPractitionerVM);

            }

            return Ok("User created Successfully");
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await _context.Users.Where(e => e.Email == loginVM.Email).FirstAsync();

            //if(result == PasswordVerificationResult.Success)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    return BadRequest("Failed login");
            //}
            return Ok("");
            
        }
        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
