using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Specialty SpecialityName { get; set; }
    }
}
