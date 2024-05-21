using DiagnosisSystem.Entities;
using DiagnosisSystem.Repositories.Interfaces;
using DiagnosisSystem.Services;
using DiagnosisSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserServices _userServices;
        public AccountRepo(ApplicationDbContext context, IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        public bool IsEmailFound(string Email)
        {
            return _context.Users.AsNoTracking().Any(e => e.Email == Email);
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
                        Speciality = d.Specialty,
                        CurrentHospital = d.CurrentHospital,
                    });
                UserDetails.AddRange(Users);
            }
            return UserDetails;
        }

        public async Task <EditProfileVM> GetAccountBasicInfo()
        {
            var UserId = _userServices.GetCurrentUserId();
            var user = await _context.Users
               .Where(i => i.Id == UserId)
               .Select(u => new EditProfileVM()
               {
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Email = u.Email,
                   Gender = u.Gender,
                   Telephone = u.Telephone,
                   ImageData = u.ImageData,
                   ImageType = u.ImageType

               }).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }
    }
}
