namespace DiagnosisSystem.Repositories
{
    public interface IQueryRepo
    {
        QueryVM GetAllAnswers(int id);
        Task<List<QueryVM>> GetSelectedPatientQueries(string userId);
        
        Task AddSpecialityToDB(Specialty specialty);
        Task AddSpecialityToDB(RegisterVM doctorVM);
        Task<List<SpecialtyVM>> GetAllSpecialties();
        
        //Task AddTagToDB(Tag tag);
        Task<List<TagVM>> GetAllTags();

        Task<List<QueryVM>> GetAllQueries();
    }
}
