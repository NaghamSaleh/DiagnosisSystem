using DiagnosisSystem.Repositories.Interfaces;
using DiagnosisSystem.Services.Interfaces;

namespace DiagnosisSystem.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserServices _userServices;
 

        public UserRepo(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IUserServices userServices)
        {
            _context = context;
            _userManager = userManager;
            _userServices= userServices;

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
        

        public List<RegisterVM> GetRequestDetails(List<string> SelectedUsers)
        {
            List<RegisterVM> UserDetails = new();
            foreach (var user in SelectedUsers)
            {
                var Users = _context.Users
                    .Where(u => u.Id == user).Select(d => new RegisterVM
                    {
                        UserID = d.Id,
                        FirstName = d.FirstName ?? d.Email,
                        LastName = d.LastName ?? "No Name saved",
                        Email = d.Email,
                        Gender = d.Gender,
                        Speciality = d.Specialty,
                        CurrentHospital = d.CurrentHospital,
                    });
                UserDetails.AddRange(Users);
            }
            return UserDetails;
        }

       
        public async Task CreateUser(User user, string password, string roleName)
        {
            try
            {
                _context.Users.Add(user);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                
                throw; 
            }
        }

        public async Task UpdateUserRole(string userId)
        {
            var entityToUpdate = _context.UserRoles.FirstOrDefault(item => item.UserId == userId);
            var roleId = await _context.Roles.Where(r => r.Name == "Doctor").Select(i => i.Id).FirstOrDefaultAsync();
            if (entityToUpdate != null)
            {
                _context.UserRoles.Remove(entityToUpdate);

                entityToUpdate.RoleId = roleId;
                entityToUpdate.UserId = userId;

                _context.UserRoles.Add(entityToUpdate);
                await _context.SaveChangesAsync();

            }
            
        }
        public async Task DeleteUser(string userId)
        {
            var entityToDelete = _context.UserRoles.FirstOrDefault(item => item.UserId == userId);
            if (entityToDelete != null)
            {
                _context.UserRoles.Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public EditProfileVM GetProfilePicture(string userId)
        {
            var user = _context.Users
                .Where(i => i.Id == userId)
                .Select(u => new EditProfileVM()
                {
                    ImageData = u.ImageData,
                    ImageType = u.ImageType

                }).FirstOrDefault();
            return user;
        }
        public async Task<User> GetUserbyId(string Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public async Task<User> UpdateUserInfo(EditProfileVM model)
        {
            var UserId = _userServices.GetCurrentUserId();
            var user = await GetUserbyId(UserId);
            var newUserInfo = await _userServices.MapUser(model, user);
            await _context.SaveChangesAsync();
            return newUserInfo;
        }
    }
}
