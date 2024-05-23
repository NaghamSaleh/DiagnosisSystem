using DiagnosisSystem.Repositories.Interfaces;
using DiagnosisSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        private readonly IAuthenticationService _authService;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRegisterRepo _registerRepo;
        private readonly IQueryRepo _queryRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IPatientServices _patientServices;

        #endregion

        #region Constructors
        public AccountController(SignInManager<IdentityUser> signInManager, IRegisterRepo registerRepo, IQueryRepo queryRepo,
            IAuthenticationService authenticationService, IUserRepo userRepo, 
            ApplicationDbContext context, IAccountRepo accountRepo, IPatientServices patientServices)
        {
            _signInManager = signInManager;
            _registerRepo = registerRepo;
            _queryRepo = queryRepo;
            _authService = authenticationService;
            _context = context;
            _userRepo = userRepo;
            _accountRepo = accountRepo;
            _patientServices = patientServices;
        }
        #endregion


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


        #region Register Doctors
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

            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Register Admin
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
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

        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            var questions = await _queryRepo.GetSelectedPatientQueries();
            var user = await _accountRepo.GetAccountBasicInfo();
            var Patient = _patientServices.MapPatientModel(user, questions);
            
            ViewData["EditProfileVM"] = user;
            return View(Patient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _accountRepo.GetAccountBasicInfo();
            
            ViewData["EditProfileVM"] = user;
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVM model)
        {
            var user = await _userRepo.UpdateUserInfo(model);


            ViewData["EditProfileVM"] = user;


            return RedirectToAction("MyAccount");
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var roles = await _authService.SignInAsync(loginVM.Email, loginVM.Password);

                if (roles != null)
                {
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
            return View();

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
