namespace DiagnosisSystem.Models
{
    public class RegisterVM
    {
        public string UserID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)] // Ensure the Date type is used in the view
        public DateTime DateOfBirth { get; set; } = DateTime.Now.Date;

        public string Telephone { get; set; } = string.Empty;
        public char Gender { get; set; } = 'N';
        public DateTime AddedOn { get; set; } = DateTime.Now.Date;
    }
}
