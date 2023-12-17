using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class PatientQuestion
    {
        [Key]
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public Tag QuestionTag { get; set; }
    }
}
