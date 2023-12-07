using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.entities
{
    public class MedicalPractitionerClass
    {
        [Key]
        public int MedicalPractitionerID { get; set; }
        public string Speciality { get; set; }
    }
}
