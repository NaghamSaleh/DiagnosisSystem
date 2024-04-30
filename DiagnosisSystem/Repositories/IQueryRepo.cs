namespace DiagnosisSystem.Repositories
{
    public interface IQueryRepo
    {
        QueryVM GetAllAnswers(string id);
        Task<List<QueryVM>> GetSelectedPatientQueries(string userId);
        
        Task AddSpecialityToDB(Specialty specialty);
        Task AddSpecialityToDB(DoctorRegisterVM doctorVM);
        Task<List<SpecialtyVM>> GetAllSpecialties();
        
        //Task AddTagToDB(Tag tag);
        Task<List<TagVM>> GetAllTags();

        Task<List<QueryVM>> GetAllQueries();
    }
}
