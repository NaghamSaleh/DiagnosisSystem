using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class TransactDoctor 
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public char Gender { get; set; }
        public string Specialty { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string CurrentHospital { get; set; }
        public string ShortBio { get; set; }
    }
}
