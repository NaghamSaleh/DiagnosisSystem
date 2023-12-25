using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Models
{
    public class RegisterVM
    {
        public string UserID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public char Gender { get; set; } = 'N';
        public DateTime AddedOn { get; set; } = DateTime.Parse(DateTime.Now.ToString(""));
    }
}
