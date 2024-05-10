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
      

        public List<DoctorDTO> GetAllDoctors()
        {
            string roleDoctor = "Doctor";
            var doctorRoleId = _context.Roles.Where(r => r.Name == roleDoctor).Select(r => r.Id).FirstOrDefault();
            var doctorId = _context.UserRoles.Where(i => i.RoleId.Equals(doctorRoleId)).Select(i => i.UserId).ToList();
            var doctorUsers = _context.Users.Where(u => doctorId.Contains(u.Id)).Select(d=> new DoctorDTO
            {
                Id = d.Id,
                Speciality=d.Specialty,
                FirstName= d.FirstName,
                LastName = d.LastName,
                Experience=d.Experience,
                Languages=d.Languages,
                CurrentHospital = d.CurrentHospital,
                ShortBio=d.ShortBio,
            }).ToList();
            return doctorUsers;
        }

        public RegisterVM GetDoctorbyId(string id)
        {
            var doctor = _context.Users.Where(d=> d.Id == id)
                .Select(d => new RegisterVM
                {
                    Speciality = d.Specialty,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    ShortBio = d.ShortBio,
                    Experience = d.Experience,
                    Languages = d.Languages,
                })
                .FirstOrDefault();

            if (doctor == null)
            {
                return null;
            }
            return doctor;
        }

        


    }
}
