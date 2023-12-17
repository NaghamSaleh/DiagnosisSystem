using DiagnosisSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DiagnosisSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Specialty> Specialities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<TransactDoctor> transactDoctors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            var admin_roleId = Guid.NewGuid().ToString();
            var user_id = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = admin_roleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),

                });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Patient",
                    NormalizedName = "Patient".ToUpper(),

                });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Doctor",
                    NormalizedName = "Doctor".ToUpper(),

                });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "InitialDoctor",
                    NormalizedName = "InitialDoctor".ToUpper(),

                });


            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>()
                .HasData(new IdentityUser
                {
                    NormalizedEmail = "naghamsaleh@gmail.com".ToUpper(),
                    Email = "naghamsaleh@gmail.com",
                    NormalizedUserName = "naghamsaleh".ToUpper(),
                    UserName = "naghamsaleh",
                    PasswordHash = hasher.HashPassword(null, "+fUY<Z0B|2b2F.Yv2l^z-"),
                    EmailConfirmed = true,
                    Id = user_id

                });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = admin_roleId,
                UserId = user_id
            });

        }
    }
}
