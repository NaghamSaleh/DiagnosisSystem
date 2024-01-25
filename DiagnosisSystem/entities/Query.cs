using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Query
    {
        [Key]
        public int Id { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string PatientId { get; set; } 
        public int Votes { get; set; }
        public List<Answer> Answers { get; set; }
        public int AnswerCount => Answers?.Count ?? 0;
    }
}
