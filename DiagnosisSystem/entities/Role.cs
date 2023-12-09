using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
