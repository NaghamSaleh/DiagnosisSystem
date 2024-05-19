using Microsoft.AspNetCore.Mvc;

namespace DiagnosisSystem.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
