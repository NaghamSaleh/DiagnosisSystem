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
        
        #endregion

        #region Constructors
        public AccountController(SignInManager<IdentityUser> signInManager, IRegisterRepo registerRepo, IQueryRepo queryRepo,
            IAuthenticationService authenticationService, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _registerRepo = registerRepo;
            _queryRepo = queryRepo;
            _authService = authenticationService;
            _context= context;
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

            return PartialView("doctorRegister", new RegisterVM());
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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var questions = await _queryRepo.GetSelectedPatientQueries(userId);

            var user = _context.Users
                .Where(i => i.Id == userId)
                .Select(u => new EditProfileVM()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Gender = u.Gender,
                    Telephone = u.Telephone,
                    ImageData = u.ImageData,
                    ImageType = u.ImageType

                }).FirstOrDefault();
            var PatientDTO = new PatientDTO();
            ViewData["EditProfileVM"] = user;


            PatientDTO.QueryVM = questions;
            PatientDTO.EditProfileVM = user;
            return View(PatientDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var user = await _context.Users
                .Where(i => i.Id == userId)
                .Select(u => new EditProfileVM()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Gender = u.Gender,
                    Telephone = u.Telephone
                }).FirstOrDefaultAsync();

            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVM model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }


            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Telephone = model.Telephone;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(memoryStream);
                    user.ImageData = memoryStream.ToArray();
                    user.ImageType = model.ImageFile.ContentType;
                }
            }

            await _context.SaveChangesAsync();

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
