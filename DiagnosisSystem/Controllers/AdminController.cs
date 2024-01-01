using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;

namespace DiagnosisSystem.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        #region Variables
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Index, Full Management
        [HttpGet]
        public IActionResult Index()
        {


            #region Get Pending Doctor Requests
            string roleNameI = "InitialDoctor";
            var role =  _context.Roles.Where(r => r.Name == roleNameI).Select(r => r.Id).FirstOrDefault();
            var userId =  _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToList();
            
            int numOfIRequests = userId.Count();

            #endregion

            #region Get Registered Doctors
            string roleDoctor = "Doctor";
            var doctorRoleId = _context.Roles.Where(r => r.Name == roleDoctor).Select(r => r.Id).FirstOrDefault();
            var doctorId = _context.UserRoles.Where(i => i.RoleId.Equals(doctorRoleId)).Select(i => i.UserId).ToList();

            int numOfDRequests = doctorId.Count();
            #endregion

            #region Get Rejected Doctors
            #endregion

            #region Get Registered Patients
            string rolePatient = "Patient";
            var patientRoleid = _context.Roles.Where(r => r.Name == rolePatient).Select(r => r.Id).FirstOrDefault();
            var patientId = _context.UserRoles.Where(i => i.RoleId.Equals(patientRoleid)).Select(i => i.UserId).ToList();

            int numOfPatients = patientId.Count();
            #endregion

            #region Get Registered Admins
            string roleAdmin = "Admin";
            var adminRoleid = _context.Roles.Where(r => r.Name == roleAdmin).Select(r => r.Id).FirstOrDefault();
            var adminId = _context.UserRoles.Where(i => i.RoleId.Equals(adminRoleid)).Select(i => i.UserId).ToList();

            int numOfAdmins = adminId.Count();
            #endregion

            return View();
        }
        #endregion

        #region Manage Doctor Account
        public async Task<IActionResult> Doctors()
        {
            string roleName = "InitialDoctor";
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
    }
}
