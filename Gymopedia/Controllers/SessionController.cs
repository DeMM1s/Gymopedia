using Microsoft.AspNetCore.Mvc;

namespace Gymopedia.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
