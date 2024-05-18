namespace DiagnosisSystem.Entities
{
    public class DiscussionAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ForumId { get; set; }
        public string DoctorName { get; set; }
        public DateTime AnsweredAt { get; set; }
        public string AnswerText { get; set; }
        public DiscussionForum Forum { get; set; }

    }
}
