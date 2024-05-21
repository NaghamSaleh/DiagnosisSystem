using DiagnosisSystem.Services.Interfaces;

namespace DiagnosisSystem.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User CreateUserEntity(RegisterVM userVM)
        {
            var user = new User
            {
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Email = userVM.Email,
                Telephone = userVM.Telephone,
                Gender = userVM.Gender,
                CreatedOn = DateTime.Today,
                UserName = userVM.Email,
                DateOfBirth = userVM.DateOfBirth,
                CurrentHospital = userVM.CurrentHospital,
                Languages = userVM.Languages,
                Specialty = userVM.Speciality,
                Experience = userVM.Experience,
                ShortBio = userVM.ShortBio,
            };

            return user;
        }

        public string GetCurrentUserName()
        {
            var User = _httpContextAccessor.HttpContext.User;
            return User?.FindFirst(ClaimTypes.Name)?.Value;
            
        }
        public string GetCurrentUserId()
        {
            var User = _httpContextAccessor.HttpContext.User;
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        }
       
        public async Task<User> MapUser(EditProfileVM model, User user)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Telephone = model.Telephone;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(memoryStream);
                    user.ImageData = memoryStream.ToArray();
                    user.ImageType = model.ImageFile.ContentType;
                }
            }

            return user;
        }
    }
}
