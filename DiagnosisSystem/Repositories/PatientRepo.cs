using DiagnosisSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;
        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public int GetPatientCount()
        {
            string rolePatient = "Patient";
            var patientRoleid = _context.Roles.Where(r => r.Name == rolePatient).Select(r => r.Id).FirstOrDefault();
            var patientId = _context.UserRoles.Where(i => i.RoleId.Equals(patientRoleid)).Select(i => i.UserId).ToList();
            return patientId.Count;
        }
    }
}
