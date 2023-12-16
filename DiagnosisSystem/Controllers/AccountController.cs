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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        #endregion

        #region Constructors
        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
                    ModelState.AddModelError("Email", "Email already exists");
                    return View();
                }

                if (userVM.Password != userVM.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password doesnt match");
                    return View();
                }

                var minDateOfBirth = DateTime.Today.AddYears(-100);
                var maxDateOfBirth = DateTime.Today.AddYears(-18);
                if (userVM.DateOfBirth < minDateOfBirth || userVM.DateOfBirth > maxDateOfBirth)
                {
                    ModelState.AddModelError("DateOfBirth", "Invalid date of birth. Must be between 18 and 100 years old.");
                    return View();
                }

                var user = new User
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Telephone = userVM.Telephone,
                    DateOfBirth = userVM.DateOfBirth,
                    Gender = userVM.Gender,
                    CreatedOn = DateTime.Now,
                    UserName = userVM.Email,
                    Password = userVM.Password,
                    ConfirmPassword = userVM.ConfirmPassword,
                };

                try
                {
                    _context.Users.Add(user);
                    var result = await _userManager.CreateAsync(user, userVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Patient");
                        _context.SaveChanges();
                        return RedirectToAction("Login", "Account"); // Redirect to login after successful registration
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error saving to database");
                }
            }

            // If ModelState is not valid, return to the registration view with validation errors
            return View(userVM);
        }

        /*[HttpGet]
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
                   CreatedOn = DateTime.Now,
                   UserName = userVM.Email
               };

                try
                {

                    _context.Users.Add(user);
                    _context.SaveChanges();
                    var result = await _userManager.CreateAsync(user, userVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Patient");
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                    
                    
                }
                catch(Exception ex)
                {
                    throw new Exception("Error saving to database");
                }
                return View(null);
            }

            return BadRequest("Retry Again please");
        }*/

        #endregion


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
                    ModelState.AddModelError("Email", "Email already exists");
                    return View();
                }

                if (MedicalPractitionerVM.Password != MedicalPractitionerVM.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password and Confirm Password do not match");
                    return View();
                }

                var minDateOfBirth = DateTime.Today.AddYears(-100);
                var maxDateOfBirth = DateTime.Today.AddYears(-18);
                if (MedicalPractitionerVM.DateOfBirth < minDateOfBirth || MedicalPractitionerVM.DateOfBirth > maxDateOfBirth)
                {
                    ModelState.AddModelError("DateOfBirth", "Invalid date of birth. Must be between 18 and 100 years old.");
                    return View();
                }

                var doctor = new User
                {
                    Email = MedicalPractitionerVM.Email,
                    FirstName = MedicalPractitionerVM.FirstName,
                    LastName = MedicalPractitionerVM.LastName,
                    DateOfBirth = MedicalPractitionerVM.DateOfBirth,
                    Gender = MedicalPractitionerVM.Gender,
                    Telephone = MedicalPractitionerVM.Telephone,
                    CurrentHospital = MedicalPractitionerVM.CurrentHospital,
                    Languages = MedicalPractitionerVM.Languages,
                    Specialty = MedicalPractitionerVM.Specialty,
                    Experience = MedicalPractitionerVM.Experience,
                    ShortBio = MedicalPractitionerVM.ShortBio,
                    CreatedOn = DateTime.Now,
                    UserName = MedicalPractitionerVM.Email,
                    Password = MedicalPractitionerVM.Password,
                    ConfirmPassword = MedicalPractitionerVM.ConfirmPassword

                };

                try
                {
                    _context.Users.Add(doctor);
                    var result = await _userManager.CreateAsync(doctor, MedicalPractitionerVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(doctor, "InitialDoctor");
                        _context.SaveChanges();
                        return RedirectToAction("Login", "Account"); // Redirect to login after successful registration
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error saving to database");
                }
            }

            // If ModelState is not valid, return to the registration view with validation errors
            return View(MedicalPractitionerVM);
        }




        #region Doctor Register 
        /*[HttpGet]
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

                var doctor = new User
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
                    CreatedOn = DateTime.Now,
                    UserName = MedicalPractitionerVM.Email
                    

                };
                
                try
                {
                    _context.Users.Add(doctor);
                    var result = await _userManager.CreateAsync(doctor, MedicalPractitionerVM.Password);
                    if (result.Succeeded)
                    {
                         await _userManager.AddToRoleAsync(doctor, "InitialDoctor");
                        _context.SaveChanges();
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving to database");
                }

                return View(null);

            }

            return Ok("User created Successfully");
        } */
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
            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, lockoutOnFailure: false);
           
            if(result.Succeeded)
            {
                var userId = _context.Users.Where(e => e.Email == loginVM.Email).Select(e => e.Id);
                var UserRole =  _context.UserRoles.Where(u => u.UserId.Equals(userId)).Select(r => r.RoleId);
                var Role = _context.Roles.Where(r => r.Id.Equals(UserRole)).Select(n => n.Name).ToString();
                if(Role == "Doctor")
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return BadRequest("Account still waiting Acceptance");
                }
                
            }
            else
            {
                return BadRequest("Failed login");
            }

       
            
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
