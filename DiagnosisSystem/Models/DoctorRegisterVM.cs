namespace DiagnosisSystem.Models
{
    public class DoctorRegisterVM : RegisterVM
    {
        public int MedicalPractitionerID { get; set; }
        public string Specialty { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string CurrentHospital { get; set; }
        public string ShortBio { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}
