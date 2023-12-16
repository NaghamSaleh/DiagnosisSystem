using DiagnosisSystem.Data;
using DiagnosisSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DiagnosisSystem.Controllers
{
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
        public async Task<IActionResult> Index()
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

    }
}
