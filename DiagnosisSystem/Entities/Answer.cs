using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QueryId { get; set; }
        [Required]
        public string AnswerBody { get; set; } 
        public string DoctorId { get; set; }
        public Query Query { get; set; }
    }


}
