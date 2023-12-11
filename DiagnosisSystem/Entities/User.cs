using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public char Gender { get; set; }
        public string Role { get; set; }
        public DateTime AddedOn { get; set; } 
    }
}
