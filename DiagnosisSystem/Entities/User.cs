namespace DiagnosisSystem.Entities
{

    public class User : IdentityUser
    {
        public DateTime CreatedOn { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public char Gender { get; set; }
        public string Specialty { get; set; }
        public int? Experience { get; set; }
        public string? Languages { get; set; }
        public string? CurrentHospital { get; set; }
        public string? ShortBio { get; set; }
        public DateTime UpdatedOn { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }



    }
}
