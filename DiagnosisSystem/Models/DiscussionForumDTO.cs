namespace DiagnosisSystem.Models
{
    public class DiscussionForumDTO
    {
        [Required(ErrorMessage = "Group Title is required")]
        [StringLength(100, ErrorMessage = "Group Title must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string GroupTitle { get; set; }

        [Required(ErrorMessage = "Related Tags are required")]
        public string RelatedTags { get; set; }

        [Required(ErrorMessage = "Group Admin is required")]
        public string GroupAdmin { get; set; }

        [Required(ErrorMessage = "Discussion Topic is required")]
        public string DiscussionTopic { get; set; }

        //public virtual ICollection<DoctorDTO> AllMembers { get; set; }
        public  List<string> SelectedMembers { get; set; }
        //public DiscussionForumDTO()
        //{
        //    AllMembers = new List<DoctorDTO>();
        //}

        //public void AddMember(DoctorDTO member)
        //{
        //    AllMembers.Add(member);
        //}
    }
}
