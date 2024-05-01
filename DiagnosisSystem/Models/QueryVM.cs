namespace DiagnosisSystem.Models
{
    public class QueryVM
    {
        public int Id { get; set; }
        
        public string? QueryTitle { get; set; }
        public string? Description { get; set; }
        public string QuestionTag { get; set; }
        public string DoctorName { get; set; } = null!;
        public int AnswerCount { get; set; }
        public int Votes { get; set; }
        public string ConsuntacyType { get; set; }
        public List<AnswerDTO>? Answers { get; set; }
        public AnswerDTO Answer { get; set; }
    }
}
