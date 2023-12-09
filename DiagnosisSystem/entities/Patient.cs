using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

    }
}
