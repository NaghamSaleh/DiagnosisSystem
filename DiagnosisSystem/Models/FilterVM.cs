namespace DiagnosisSystem.Models
{
    public class FilterVM
    {
        
        public string? SpecilityName { get; set; }
        public string? Name { get; set; }
        public string? CurrentHospital { get; set; }
        public int? Experience { get; set; }
        public string? Languages { get; set; }
        public List<DoctorDTO> Doctors { get; set; }

    }
}
