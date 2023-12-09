using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Specialty
    {
        [Key]

        //TODO: edit i
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }


    }
}
