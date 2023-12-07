using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class MedicalPractitionerClass
    {
        [Key]
        public int MedicalPractitionerID { get; set; }
        public string Speciality { get; set; }
    }
}
