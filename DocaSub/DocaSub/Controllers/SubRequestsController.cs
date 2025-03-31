using DocaSub.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocaSub.Controllers
{
    public class SubRequestsController : Controller
    {
        [Route("mes-demandes")]
        public IActionResult Index()
        {
            var liste = new List<SubRequest>
            {
                new SubRequest { Id = 1, Title = "Demande 1", Amount = 1000, Status = 1, CreatedAt=DateTime.Now },
                new SubRequest { Id = 2, Title = "Demande 2", Amount = 2000, Status = 2, CreatedAt=DateTime.Now },
                new SubRequest { Id = 3, Title = "Demande 3", Amount = 3000, Status = 3, CreatedAt=DateTime.Now }
            };
            return View(liste);
        }
    }
}
