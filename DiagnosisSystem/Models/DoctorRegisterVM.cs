namespace DiagnosisSystem.Models
{
    public class DoctorRegisterVM : RegisterVM
    {
        public string Specialty { get; set; }
        public int Experience { get; set; }
        public string Languages { get; set; }
        public string CurrentHospital { get; set; }
        public string ShortBio { get; set; }

        public string Message { get; set; }
       

    }
}
