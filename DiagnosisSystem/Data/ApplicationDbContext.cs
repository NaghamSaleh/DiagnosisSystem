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
        public DbSet<AdministratorClass> Administrators { get; set; }
        public DbSet<DistrictClass> Districts { get; set; }
        public DbSet<QueryClass> Queries { get; set; }
        public DbSet<MedicalPractitionerClass> MedicalPractitioners { get; set; }
        public DbSet<SpecialityClass> Specialities { get; set; }
        public DbSet<PatientClass> Patients { get; set; }


    }
}
