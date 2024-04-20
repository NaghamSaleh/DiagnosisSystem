namespace DiagnosisSystem.Controllers
{
    [Authorize(Roles ="Patient")]
    public class PaymentController : Controller
    {
        public IActionResult Index(string doctorId)
        {
            return View(doctorId);
        }
    }
}
