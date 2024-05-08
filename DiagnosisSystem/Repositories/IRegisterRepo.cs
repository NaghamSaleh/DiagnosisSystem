using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Repositories
{
    public interface IRegisterRepo
    {
        Task CreateAsync(RegisterVM MedicalPractitionerVM, string RoleName);
        
    }
    public class RegiserRepo : IRegisterRepo
    {
        private readonly IAccountServices _accountServices;
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _context;
        private readonly IQueryRepo _queryRepo;
        private readonly IUserRepo _userRepo;


        public RegiserRepo(IAccountServices accountServices, IUserServices userServices,
            ApplicationDbContext context, IQueryRepo queryRepo, IUserRepo userRepo)
        {
            _accountServices = accountServices;
            _userServices = userServices;
            _context = context;
            _queryRepo = queryRepo;
            _userRepo = userRepo;
        }

        public async Task CreateAsync(RegisterVM MedicalPractitionerVM, string RoleName)
        {
            var isRegistered = _accountServices.IsRegisterValid(MedicalPractitionerVM);
            if (!isRegistered)
            {
                var doctor = _userServices.CreateUserEntity(MedicalPractitionerVM);
                
                await _userRepo.CreateUser(doctor, MedicalPractitionerVM.Password, RoleName);
            }

        }
    }
}
