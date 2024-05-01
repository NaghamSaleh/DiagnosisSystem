namespace DiagnosisSystem.Models
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public int QueryId { get; set; }
        public string AnswerBody { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}
