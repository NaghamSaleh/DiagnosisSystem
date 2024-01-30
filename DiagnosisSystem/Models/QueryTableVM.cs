namespace DiagnosisSystem.Models
{
    public class QueryTableVM
    {
        public QuerySearchFilter filters { get; set; } 
        public List<QueryVM> Queries { get; set; }
    }
}
