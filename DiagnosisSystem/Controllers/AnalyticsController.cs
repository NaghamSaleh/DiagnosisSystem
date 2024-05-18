using Microsoft.EntityFrameworkCore;

namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles = "Doctor, Patient")]
    public class AnalyticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorRepo _doctorRepo;

        public AnalyticsController(ApplicationDbContext context, IDoctorRepo doctorRepo)
        {
            _context = context;
            _doctorRepo = doctorRepo;
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

            ViewBag.Specialties = specialties;
            ViewBag.YAxis = yAxis.ToList();

            return View(yAxis);
        }
    }
}
