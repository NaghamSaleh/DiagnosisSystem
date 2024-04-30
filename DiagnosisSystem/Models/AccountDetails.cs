namespace DiagnosisSystem.Models
{
    public class AccountDetails
    {
        public string UserID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public char? Gender { get; set; }
        public string? Speciality { get; set; }
        public string? CurrentHospital { get; set; }
        public string? Telephone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ShortBio { get; set; }
        public int? Experience { get; set; }
        public string? Languages { get; set; }


    }
}
