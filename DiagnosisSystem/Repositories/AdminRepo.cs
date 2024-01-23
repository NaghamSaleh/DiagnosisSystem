using DiagnosisSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        public AdminRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public int GetAdminCount()
        {
            string roleAdmin = "Admin";
            var adminRoleid = _context.Roles.Where(r => r.Name == roleAdmin).Select(r => r.Id).FirstOrDefault();
            var adminId = _context.UserRoles.Where(i => i.RoleId.Equals(adminRoleid)).Select(i => i.UserId).ToList();
            return adminId.Count;
        }
    }
}
