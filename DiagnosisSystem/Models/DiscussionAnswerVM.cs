namespace DiagnosisSystem.Models
{
    public class DiscussionAnswerVM
    {
        public Guid Id { get; set; }
        public Guid ForumId { get; set; }
        public string DoctorName { get; set; }
        public DateTime AnsweredAt { get; set; }
        public string AnswerText { get; set; }
        public DiscussionForumDTO Forum { get; set; }
    }
}
