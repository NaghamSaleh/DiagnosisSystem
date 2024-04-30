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
            };

            return user;
        }
        public User CreateUserEntity(DoctorRegisterVM doctorVM)
        {
            var doctor = new User
            {
                Email = doctorVM.Email,
                FirstName = doctorVM.FirstName,
                LastName = doctorVM.LastName,
                DateOfBirth = doctorVM.DateOfBirth,
                Gender = doctorVM.Gender,
                Telephone = doctorVM.Telephone,
                CurrentHospital = doctorVM.CurrentHospital,
                Languages = doctorVM.Languages,
                Specialty = doctorVM.Specialty,
                Experience = doctorVM.Experience,
                ShortBio = doctorVM.ShortBio,
                CreatedOn = DateTime.Now,
                UserName = doctorVM.Email,
            };
            return doctor;
        }
    }
}
