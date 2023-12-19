using DiagnosisSystem.Entities;

namespace DiagnosisSystem.Models
{
    public class TagVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpecialityVM> SpecialityName { get; set; } = new List<SpecialityVM>();
        public string SelectedSpeciality { get; set; } = string.Empty;
    }
}
