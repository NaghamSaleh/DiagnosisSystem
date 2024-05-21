using DiagnosisSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Doctor, Patient")]
    public class AnalyticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IUserRepo _userRepo;

        public AnalyticsController(ApplicationDbContext context, IDoctorRepo doctorRepo, IUserRepo userRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
            _userRepo = userRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Index()
        {

            var specialties = await _context.Specialities.Distinct().ToListAsync();
            List<DoctorDTO> doctors = _doctorRepo.GetAllDoctors();
            var doctorIds = doctors.Select(d => d.Id);

            var yAxis = await _context.Users
                .Where(e => doctorIds.Contains(e.Id))
                .GroupBy(u => u.Specialty)
                .Select(g => new AnalyticsDTO { SpecialityName = g.Key, Count = g.Count() })
                .ToListAsync();
            /*
             Select SpecialityName,  Count(Doctor)
            from User
            where id = id
            Group by 
             
             */

            ViewBag.Specialties = specialties;
            ViewBag.YAxis = yAxis.ToList();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepo.GetProfilePicture(userId);
            ViewData["EditProfileVM"] = user;

            return View(yAxis);
        }
    }
}
