using DiagnosisSystem.Entities;
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
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<MedicalPractitioner> MedicalPractitioners { get; set; }
        public DbSet<Specialty> Specialities { get; set; }
        public DbSet<Patient> Patients { get; set; }


    }
}
