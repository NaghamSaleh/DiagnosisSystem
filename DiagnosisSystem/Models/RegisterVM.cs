using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Models
{
    public class RegisterVM
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public char Gender { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
