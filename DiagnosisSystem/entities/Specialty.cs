using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Specialty
    {
        [Key]
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }


    }
}
