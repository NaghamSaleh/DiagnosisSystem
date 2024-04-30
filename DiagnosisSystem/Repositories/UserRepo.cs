using DiagnosisSystem.Entities;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Printing;
using System.Numerics;

namespace DiagnosisSystem.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepo(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int GetRoleCount(string RoleName)
        {
            var role = _context.Roles.AsNoTracking().Where(r => r.Name == RoleName).Select(r => r.Id).FirstOrDefault();
            var userId = _context.UserRoles.AsNoTracking().Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToList();
            return userId.Count;
        }
        public async Task<List<string>> GetAllUsers(string RoleName)
        {
            var role = await _context.Roles.Where(r => r.Name == RoleName).Select(r => r.Id).FirstOrDefaultAsync();
            var usersList = await _context.UserRoles.Where(i => i.RoleId.Equals(role)).Select(i => i.UserId).ToListAsync();
            return usersList;
        }
        public List<AccountDetails> GetAccountDetails(List<string> SelectedUsers)
        {
            List<AccountDetails> UserDetails = new();
            foreach (var user in SelectedUsers)
            {
                var Users = _context.Users
                    .Where(u => u.Id == user).Select(d => new AccountDetails
                    {
                        UserID = d.Id,
                        FirstName = d.FirstName ?? d.Email,
                        LastName = d.LastName ?? "No Name saved",
                        Email = d.Email,
                        Gender = d.Gender,
                    });
                UserDetails.AddRange(Users);
            }
            return UserDetails;
        }


        //might move to accountrepo
        public bool IsEmailFound(string Email)
        {
            return  _context.Users.AsNoTracking().Any(e => e.Email == Email);
        }
        public async Task CreateUser(User user, string password, string roleName)
        {
            _context.Users.Add(user);
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
                _context.SaveChanges();

            }
        }
    }
}
