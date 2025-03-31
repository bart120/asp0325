using Microsoft.AspNetCore.Mvc;

namespace DocaSub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
