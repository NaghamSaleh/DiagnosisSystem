using DiagnosisSystem.entities;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UsersClass> Users { get; set; }
        public DbSet<RoleClass> Roles { get; set; }
    }
}
