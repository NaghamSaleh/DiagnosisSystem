using DiagnosisSystem.Entities;

namespace DiagnosisSystem.Models
{
    public class PatientQuestionVM
    {
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public TagVM QuestionTag { get; set; }
    }
}
