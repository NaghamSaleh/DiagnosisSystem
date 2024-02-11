using DiagnosisSystem.Data;
using DiagnosisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public class DoctorRepo : IDoctorRepo
    {
        #region Variables
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public DoctorRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        public int GetDrPendingRequestsCount()
        {
            string roleNameI = "InitialDoctor";
            var role = _context.Roles.Where(r => r.Name == roleNameI).Select(r => r.Id).FirstOrDefault();
            var userId = _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToList();
            return userId.Count;
        }

        public int GetRegisteredDrCount()
        {
            string roleDoctor = "Doctor";
            var doctorRoleId = _context.Roles.Where(r => r.Name == roleDoctor).Select(r => r.Id).FirstOrDefault();
            var doctorId = _context.UserRoles.Where(i => i.RoleId.Equals(doctorRoleId)).Select(i => i.UserId).ToList();
            return doctorId.Count;
        }

        public List<DoctorRegisterVM> GetAllDoctors()
        {
            string roleDoctor = "Doctor";
            var doctorRoleId = _context.Roles.Where(r => r.Name == roleDoctor).Select(r => r.Id).FirstOrDefault();
            var doctorId = _context.UserRoles.Where(i => i.RoleId.Equals(doctorRoleId)).Select(i => i.UserId).ToList();
            var doctorUsers = _context.Users.Where(u => doctorId.Contains(u.Id)).Select(d=> new DoctorRegisterVM
            {
                UserID = d.Id,
                Specialty=d.Specialty,
                FirstName= d.FirstName,
                LastName = d.LastName,
            }).ToList();
            return doctorUsers;
        }
        
    }
}
