using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using DiagnosisSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace DiagnosisSystem.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        #region Variables
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructors
        public AdminController(ApplicationDbContext context, IDoctorRepo doctorRepo, 
            IPatientRepo patientRepo, IAdminRepo accountRepo, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
            _adminRepo = accountRepo;
            _userManager = userManager;
        }
        #endregion

        #region Index, Full Management
        [HttpGet]
        public IActionResult Index()
        {
            Stats stats = new();
            stats.numOfIRequests = _doctorRepo.GetDrPendingRequestsCount();
            stats.numOfDoctors = _doctorRepo.GetRegisteredDrCount();
            //TODO: Get Rejected Doctors
            stats.numOfPatients = _patientRepo.GetPatientCount();
           
            
            stats.numOfAdmins = _adminRepo.GetAdminCount();
            var admin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fullName = _adminRepo.GetAdminUsername(admin);
            

            return View(stats);
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Admins()
        {
            string roleName = "Admin";
            List<DoctorRegisterVM> registeredAdmins = new();
            var role = await _context.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefaultAsync();
            var userId = await _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToListAsync();
            foreach (var user in userId)
            {
                var rAdmins = _context.Users
                    .Where(u => u.Id == user).Select(d => new DoctorRegisterVM
                    {
                        UserID = d.Id,
                        FirstName = d.FirstName ?? d.Email,
                        LastName = d.LastName ?? "No Name saved",
                        Email = d.Email ,
                        
                    });
                registeredAdmins.AddRange(rAdmins);
            }
            return View(registeredAdmins);
        }


        #region Manage Doctor Account
        [HttpGet]
        public async Task<IActionResult> Doctors()
        {
            string roleName = "Doctor";
            List<DoctorRegisterVM> doctorRegisterVMs= new List<DoctorRegisterVM>();
            var role = await _context.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefaultAsync();
            var userId = await _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToListAsync();
            foreach(var user in userId)
            {
                var iDoctors = _context.Users
                    .Where(u => u.Id == user)
                    .Select(d => new DoctorRegisterVM
                    {
                        UserID = d.Id,
                        AddedOn = d.CreatedOn,
                        CurrentHospital = d.CurrentHospital,
                        DateOfBirth = d.DateOfBirth,
                        Email = d.Email,
                        Experience = d.Experience,
                        FirstName = d.FirstName,
                        Gender = d.Gender,
                        LastName = d.LastName,
                        Languages = d.Languages,
                        ShortBio = d.ShortBio,
                        Specialty = d.Specialty,
                        Telephone = d.Telephone,
                    });
                doctorRegisterVMs.AddRange(iDoctors);
            }
            return View(doctorRegisterVMs);
        }

        
        public async Task<IActionResult> Requests()
        {
            string roleName = "InitialDoctor";
            List<DoctorRegisterVM> doctorRegisterVMs = new List<DoctorRegisterVM>();
            var role = await _context.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefaultAsync();
            var userId = await _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToListAsync();
            foreach (var user in userId)
            {
                var iDoctors = _context.Users
                    .Where(u => u.Id == user)
                    .Select(d => new DoctorRegisterVM
                    {
                        UserID = d.Id,
                        AddedOn = d.CreatedOn,
                        CurrentHospital = d.CurrentHospital,
                        DateOfBirth = d.DateOfBirth,
                        Email = d.Email,
                        Experience = d.Experience,
                        FirstName = d.FirstName,
                        Gender = d.Gender,
                        LastName = d.LastName,
                        Languages = d.Languages,
                        ShortBio = d.ShortBio,
                        Specialty = d.Specialty,
                        Telephone = d.Telephone,
                    });
                doctorRegisterVMs.AddRange(iDoctors);
            }
            return View(doctorRegisterVMs);
        }

        public IActionResult Approve(string userId)
        {
            var entityToUpdate =  _context.UserRoles.FirstOrDefault(item => item.UserId == userId);
            var roleId =  _context.Roles.Where(r => r.Name == "Doctor").Select(i => i.Id).FirstOrDefault();
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
        #endregion

        #region Add Speciality
        [HttpGet]
        public IActionResult Speciality()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Speciality(SpecialityVM specialityVM)
        {
            if (ModelState.IsValid)
            {
                var speciality = new Specialty()
                {
                    SpecialtyName = specialityVM.Name,
                    Description = specialityVM.Description
                };

                _context.Specialities.Add(speciality);
                _context.SaveChanges();
                return Ok("Successfully added");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSpeciality(int id)
        {
            if(id == null || _context.Specialities == null)
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
        public async Task<IActionResult> EditSpeciality(int id, SpecialityVM specialityVM)
        {
            if(id != specialityVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var SpecialityEntity = await _context.Specialities.Where(s => s.SpecialtyID == id).FirstAsync();
                    if(SpecialityEntity == null)
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

        public IActionResult ViewSpecialities()
        {
            var allSpecialities = _context.Specialities
                .Select(t => new SpecialityVM
                {
                    Name = t.SpecialtyName,
                    Description = t.Description,
                    
                }).ToList();

            return View(allSpecialities);
        }

        #endregion

        #region Add Tag
        [HttpGet]
        public IActionResult Tag()
        {
            var tagVM = new TagVM();
            tagVM.SpecialityName = _context.Specialities.Select(s => new SpecialityVM
            {
                Description = s.Description,
                Name = s.SpecialtyName,
                Id = s.SpecialtyID,
            }).ToList();
           
           
            return View(tagVM);
        }

        [HttpPost]
        public async Task<IActionResult> Tag(TagVM tagVM)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag()
                {
                    Name = tagVM.Name,
                    Description = tagVM.Description,
                    SpecialityName = tagVM.SelectedSpeciality
                };
                _context.Tags.Add(tag);
                _context.SaveChanges();
            }
            return Ok("Successfully Saved");
        }

        public IActionResult ViewTags()
        {
            var allTags = _context.Tags
                .Select(t => new TagVM
                {
                    Name = t.Name,
                    Description = t.Description,
                    SelectedSpeciality = t.SpecialityName
                }).ToList();
            return View(allTags);
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Patients()
        {
            string roleName = "Patient";
            List<DoctorRegisterVM> registeredAdmins = new();
            var role = await _context.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefaultAsync();
            var userId = await _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToListAsync();
            foreach (var user in userId)
            {
                var rAdmins = _context.Users
                    .Where(u => u.Id == user).Select(d => new DoctorRegisterVM
                    {
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Email = d.Email,
                        Gender = d.Gender,
                    });
                registeredAdmins.AddRange(rAdmins);

            }
            return View(registeredAdmins);
        }


    }
}
