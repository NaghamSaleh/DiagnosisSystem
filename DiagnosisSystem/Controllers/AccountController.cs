using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using DiagnosisSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace DiagnosisSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountServices _accountServices;
        private readonly IUserServices _userServices;
        #endregion

        #region Constructors
        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IAccountServices accountServices,
            IUserServices userServices)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServices = accountServices;
            _userServices = userServices;
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

                if (!_accountServices.ValidBirthDate(userVM.DateOfBirth))
                {
                    ModelState.AddModelError("DateOfBirth", "Invalid date of birth. Must be between 18 and 100 years old.");
                    return View();
                }

                var user = _userServices.CreateUserEntity(userVM);
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
                    ModelState.AddModelError(ex.Message, "Error saving to database");
                }
            }

            // If ModelState is not valid, return to the registration view with validation errors
            return View(MedicalPractitionerVM);
        }
        #endregion

        #region Register Admin
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _context.Users.Any(e => e.Email == registerVM.Email);

                if (checkEmail)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View();
                }

                if (registerVM.Password != registerVM.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password and Confirm Password do not match");
                    return View();
                }
                var admin = new User
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    //Email = registerVM.Email,
                    UserName = registerVM.Email

                };
                try
                {
                    _context.Users.Add(admin);
                    var result = await _userManager.CreateAsync(admin, registerVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "Admin");
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
                    ModelState.AddModelError(ex.Message, "Error saving to database");
                }
                
            }
            return View();
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
            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userId = await _context.Users.Where(e => e.UserName == loginVM.Email).Select(e => e.Id).FirstOrDefaultAsync();
                var UserRole = await _context.UserRoles.Where(u => u.UserId == userId).Select(r => r.RoleId).FirstOrDefaultAsync();
                var Role = await _context.Roles.Where(r => r.Id == UserRole).Select(n => n.Name).FirstOrDefaultAsync();
                //if(_userManager.IsInRoleAsync(userId, "Doctor")) { }
                //var userId = _context.Users.Where(e => e.Email == loginVM.Email)
                //    .Select(e => new IdentityUser
                //    {
                //        Email = e.Email,
                //        PasswordHash = e.PasswordHash,
                //    });
                if (Role.Equals("Doctor"))
                {
                    return RedirectToAction("Login", "Account");
                }
                else if(Role.Equals("InitialDoctor"))
                {
                    return BadRequest("Account still waiting Acceptance");
                }
                else if(Role.Equals("Patient"))
                {
                    return RedirectToAction("Index", "Patient");
                }
                else if(Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return BadRequest("Invalid Role");
                }
            }
            else
            {
                return BadRequest("Failed");
            }
        }


        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
