namespace DiagnosisSystem.Models
{
    public class TagVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpecialtyVM> SpecialityName { get; set; } = new List<SpecialtyVM>();
        public string SelectedSpeciality { get; set; } = string.Empty;
    }
}
