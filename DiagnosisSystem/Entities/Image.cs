namespace DiagnosisSystem.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }


    }
}
