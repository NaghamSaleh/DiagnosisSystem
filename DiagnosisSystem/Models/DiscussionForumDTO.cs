namespace DiagnosisSystem.Models
{
    public class DiscussionForumDTO
    {
        [Required(ErrorMessage = "Group Title is required")]
        [StringLength(100, ErrorMessage = "Group Title must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string GroupTitle { get; set; } = null!;

        [Required(ErrorMessage = "Related Tags are required")]
        public string RelatedTags { get; set; }

        [Required(ErrorMessage = "Group Admin is required")]
        public string GroupAdmin { get; set; } = null!;

        [Required(ErrorMessage = "Discussion Topic is required")]
        public string DiscussionTopic { get; set; } 
        public  List<string> SelectedMembers { get; set; }
       
    }
}
