namespace DiagnosisSystem.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IUserRepo _userRepo;
        public AccountServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public Stats GetAccountsStats()
        {
            Stats stats = new()
            {
                numOfIRequests = _userRepo.GetRoleCount("InitialDoctor"),
                numOfDoctors = _userRepo.GetRoleCount("Doctor"),
                numOfPatients = _userRepo.GetRoleCount("Patient"),
                numOfAdmins = _userRepo.GetRoleCount("Admin") - 1
            };
            return stats;
        }

        public bool ValidBirthDate(DateTime birthDate)
        {
            var minDateOfBirth = DateTime.Today.AddYears(-100);
            var maxDateOfBirth = DateTime.Today.AddYears(-18);
            var userDateOfBirth = birthDate.Date;
            if (userDateOfBirth < minDateOfBirth || userDateOfBirth > maxDateOfBirth)
                return false;
            return true;
        }

        private bool ValidPassword(string password, string confirmPassword)
        {
            if(password != confirmPassword) return false;
            return true;
        }
        public bool IsRegisterValid(RegisterVM userVM)
        {
            if(_userRepo.IsEmailFound(userVM.Email) is false)
            {
                return false;
            }
            if(ValidPassword(userVM.Password, userVM.ConfirmPassword) is false)
            {
                return false;
            }
            if(ValidBirthDate(userVM.DateOfBirth) is false)
            {
                return false;
            }
            return true;
        }
    }
}
