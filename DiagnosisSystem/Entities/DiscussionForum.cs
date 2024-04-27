namespace DiagnosisSystem.Entities
{
    public class DiscussionForum
    {
        [Key]
        public Guid Id { get; set; }

        public string DiscussionTopic { get; set; }
        public string GroupTitle { get; set; }
        public string GroupAdmin { get; set; } 
        public string SelectedMembers { get; set; } 
    }
}
