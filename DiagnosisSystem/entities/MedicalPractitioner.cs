using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class MedicalPractitioner : User
    {
        public int MedicalPractitionerID { get; set; }
        public string Specialty { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string CurrentHospital { get; set; }
        public string ShortBio { get; set; }




    }
}
