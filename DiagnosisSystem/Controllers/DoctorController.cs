using Microsoft.AspNetCore.Mvc;

namespace DiagnosisSystem.Controllers
{
    public class DoctorController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
