namespace DiagnosisSystem.Models
{
    public class EditProfileVM
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public string Telephone { get; set; }
        public IFormFile ImageFile { get; set; }
        public byte[] ImageData { get; set; } // Add this property
        public string ImageType { get; set; } // Add this property
    }
}
