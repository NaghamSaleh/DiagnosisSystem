namespace DiagnosisSystem.Models
{
    public class DoctorDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Speciality { get; set; }
        public int? Experience { get; set; }
        public string? Languages { get; set; }
        public string? CurrentHospital { get; set; }
        public string? ShortBio { get; set; }

    }
}
