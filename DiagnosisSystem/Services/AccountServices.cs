using DiagnosisSystem.Entities;

namespace DiagnosisSystem.Services
{
    public class AccountServices : IAccountServices
    {
        public bool ValidBirthDate(DateTime birthDate)
        {
            var minDateOfBirth = DateTime.Today.AddYears(-100);
            var maxDateOfBirth = DateTime.Today.AddYears(-18);
            var userDateOfBirth = birthDate.Date;
            if (userDateOfBirth < minDateOfBirth || userDateOfBirth > maxDateOfBirth)
                return false;
            return true;
        }
    }
}
