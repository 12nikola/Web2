using Microsoft.AspNetCore.Mvc;

namespace KvizHub.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
