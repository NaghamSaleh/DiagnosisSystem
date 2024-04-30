namespace DiagnosisSystem.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        #region Variables
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly IAccountServices _accountServices;
        private readonly IQueryRepo _queryRepo;
        private readonly IQueryServices _queryServices;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructor
        public AdminController(ApplicationDbContext context, IDoctorRepo doctorRepo,
            IPatientRepo patientRepo, IAdminRepo accountRepo,
            UserManager<IdentityUser> userManager, IUserRepo userRepo,
            IQueryRepo queryRepo, IQueryServices queryServices, IAccountServices accountServices)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _adminRepo = accountRepo;
            _userManager = userManager;
            _userRepo = userRepo;
            _queryRepo = queryRepo;
            _queryServices = queryServices;
            _accountServices = accountServices;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            var stats = _accountServices.GetAccountsStats();
            var admin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            stats.UserName = _adminRepo.GetAdminUsername(admin);
            return View(stats);
        }

        [HttpGet]
        public async Task<IActionResult> Admins()
        {
            var AllAdmins = await _userRepo.GetAllUsers("Admin");
            var AdminDetails = _userRepo.GetAccountDetails(AllAdmins);
            return View(AdminDetails);
        }


        [HttpGet]
        public async Task<IActionResult> Doctors()
        {
            var AllDoctors = await _userRepo.GetAllUsers("Doctor");
            var DoctorDetails = _userRepo.GetAccountDetails(AllDoctors);
            return View(DoctorDetails);
        }

        public async Task<IActionResult> Requests()
        {
            var AllDoctors = await _userRepo.GetAllUsers("InitialDoctor");
            var DoctorDetails = _userRepo.GetAccountDetails(AllDoctors);
            return View(DoctorDetails);
        }

        public IActionResult Approve(string userId)
        {
            var entityToUpdate = _context.UserRoles.FirstOrDefault(item => item.UserId == userId);
            var roleId = _context.Roles.Where(r => r.Name == "Doctor").Select(i => i.Id).FirstOrDefault();
            if (entityToUpdate != null)
            {
                _context.UserRoles.Remove(entityToUpdate);
                _context.SaveChanges();
                entityToUpdate.RoleId = roleId;
                entityToUpdate.UserId = userId;

                _context.UserRoles.Add(entityToUpdate);

                // Save changes to the database
                _context.SaveChanges();

            }

            return Ok("Successfully Updated");
        }
        //add role = rejected
        public IActionResult Reject(string userId)
        {
            var entityToDelete = _context.UserRoles.FirstOrDefault(item => item.UserId == userId);
            if (entityToDelete != null)
            {
                _context.UserRoles.Remove(entityToDelete);
                _context.SaveChanges();
            }

            return Ok("Successfully Deleted and Rejected");
        }


        [HttpGet]
        public async Task<IActionResult> Patients()
        {
            var AllPatients = await _userRepo.GetAllUsers("Patient");
            var PatientDetails = _userRepo.GetAccountDetails(AllPatients);

            return View(PatientDetails);
        }

        [HttpGet]
        public IActionResult Speciality()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Speciality(SpecialtyVM specialityVM)
        {
            if (ModelState.IsValid)
            {
                var speciality = _queryServices.ConvertToEntity(specialityVM.Name, specialityVM.Description);
                await _queryRepo.AddSpecialityToDB(speciality);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSpeciality(int id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }
            var specialityInfo = await _context.Specialities.Where(s => s.SpecialtyID == id).SingleOrDefaultAsync();
            var specialityVM = new Specialty()
            {
                SpecialtyID = specialityInfo.SpecialtyID,
                SpecialtyName = specialityInfo.SpecialtyName,
                Description = specialityInfo.Description
            };

            return View(specialityVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditSpeciality(int id, SpecialtyVM specialityVM)
        {
            if (id != specialityVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var SpecialityEntity = await _context.Specialities.Where(s => s.SpecialtyID == id).FirstAsync();
                    if (SpecialityEntity == null)
                    {
                        throw new ArgumentException(nameof(SpecialityEntity));
                    }
                    else
                    {
                        SpecialityEntity.Description = specialityVM.Description;
                        _context.Update(specialityVM);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));

            }
            return View(specialityVM);
        }

        public async Task<IActionResult> ViewSpecialities()
        {
            var allSpecialties = await _queryRepo.GetAllSpecialties();
            return View(allSpecialties);
        }

        [HttpGet]
        public async Task<IActionResult> Tag()
        {
            var tagVM = new TagVM
            {
                SpecialityName = await _queryRepo.GetAllSpecialties()
            };
            return View(tagVM);
        }

        [HttpPost]
        public async Task<IActionResult> Tag(TagVM tagVM)
        {
            if (ModelState.IsValid)
            {
                var tag = _queryServices.ConvertToEntity(tagVM);
                try
                {
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ViewTags()
        {
            var allTags = _queryRepo.GetAllTags();
            return View(allTags);
        }





    }
}
