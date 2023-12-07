using System.ComponentModel.DataAnnotations;

namespace DiagnosisSystem.Entities
{
    public class DistrictClass
    {
        [Key]
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


    }
}
