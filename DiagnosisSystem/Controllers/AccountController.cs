using DiagnosisSystem.Entities;

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
        private readonly IRegisterRepo _registerRepo;
        private readonly IQueryRepo _queryRepo;
        #endregion

        #region Constructors
        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IAccountServices accountServices,
            IUserServices userServices, IRegisterRepo registerRepo, IQueryRepo queryRepo)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServices = accountServices;
            _userServices = userServices;
            _registerRepo = registerRepo;
            _queryRepo = queryRepo;
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
                await _registerRepo.CreateAsync(userVM, "Patient");
                return RedirectToAction("Login");
            }
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
        public async Task<IActionResult> DoctorRegister(RegisterVM MedicalPractitionerVM)
        {
            await _registerRepo.CreateAsync(MedicalPractitionerVM, "InitialDoctor");
            await _queryRepo.AddSpecialityToDB(MedicalPractitionerVM);

            return PartialView("doctorRegister", new RegisterVM());
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
                await _registerRepo.CreateAsync(registerVM, "Admin");
                return View();
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
                    HttpContext.Session.SetString("Username", loginVM.Email);
                    return roles.FirstOrDefault() switch
                    {
                        "Doctor" => RedirectToAction("Queries", "Doctor"),
                        "InitialDoctor" => BadRequest("Account still waiting Acceptance"),
                        "Patient" => RedirectToAction("Queries", "Patient"),
                        "Admin" => RedirectToAction("Index", "Admin"),
                        _ => BadRequest("Invalid Role"),
                    };
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
       
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
