using Microsoft.AspNetCore.Mvc;

namespace DocaSub.Areas.Backoffice.Controllers
{
    [Area("Backoffice")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
