namespace DiagnosisSystem.Services
{
    public class UserServices : IUserServices
    {
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
       
    }
}
