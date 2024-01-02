namespace DiagnosisSystem.Models
{
    public class PatientQuestionVM
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public List<string> QuestionTag { get; set; }
    }
}
