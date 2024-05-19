using Microsoft.AspNetCore.Mvc;

namespace DiagnosisSystem.Controllers
{
    public class QueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
