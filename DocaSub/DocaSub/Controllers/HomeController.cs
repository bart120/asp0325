using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace DocaSub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["Title"] = "Home";
            //ViewBag.Title = "Home";
            return View();
        }
    }
}
