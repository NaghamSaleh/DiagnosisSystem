using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.entities
{
    public class QueryClass
    {
        [Key]
        public int QueryID { get; set; }
        public string Question { get; set; }
        public string Speciality { get; set; }


    }
}
