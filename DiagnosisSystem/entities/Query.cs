using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Query
    {
        [Key]
        public int QueryID { get; set; }
        public string Question { get; set; }
        public string Specialty { get; set; }


    }
}
