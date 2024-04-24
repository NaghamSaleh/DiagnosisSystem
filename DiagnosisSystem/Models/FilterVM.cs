namespace DiagnosisSystem.Models
{
    public class FilterVM<T>
    {
        public FilterVM()
        {
            Results = new List<T>();

        }
        public string SpecilityName { get; set; }
        public List<T> Results { get; set; }
    }
}
