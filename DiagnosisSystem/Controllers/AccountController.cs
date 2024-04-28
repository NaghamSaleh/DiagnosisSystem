namespace DiagnosisSystem.Controllers
{
    [AllowAnonymous]
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
        public IActionResult DoctorRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRegister(DoctorRegisterVM MedicalPractitionerVM)
        {
            //if (ModelState.IsValid)
            //{
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
                var specialities = _context.Specialities.Select(s=> s.SpecialtyName);
                if (!specialities.Contains(MedicalPractitionerVM.Specialty))
                {
                    var newSpeciality = new Specialty
                    {
                        SpecialtyName = MedicalPractitionerVM.Specialty,

                        Description = string.Empty,
                    };
                    _context.Specialities.Add(newSpeciality);
                    _context.SaveChanges();
                }

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
            //}

            // If ModelState is not valid, return to the registration view with validation errors
            return View(MedicalPractitionerVM);
        }
        #endregion

        #region Register Admin
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        public async Task<IActionResult> CreateAdmin(RegisterVM registerVM)
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
                    Email = registerVM.Email,
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
            if (!ModelState.IsValid)
            {
                // If model state is not valid, return the view with validation errors
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    switch (roles.FirstOrDefault())
                    {
                        case "Doctor":
                            return RedirectToAction("Queries", "Doctor");
                        case "InitialDoctor":
                            return BadRequest("Account still waiting Acceptance");
                        case "Patient":
                            return RedirectToAction("Queries", "Patient");
                        case "Admin":
                            return RedirectToAction("Index", "Admin");
                        default:
                            return BadRequest("Invalid Role");
                    }
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            else
            {
                // Authentication failed, return error message
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginVM);
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
