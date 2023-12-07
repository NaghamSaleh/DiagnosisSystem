using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class QueryClass
    {
        [Key]
        public int QueryID { get; set; }
        public string Question { get; set; }
        public string Speciality { get; set; }


    }
}
