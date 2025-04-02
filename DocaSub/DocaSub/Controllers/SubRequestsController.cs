using DocaSub.Data;
using DocaSub.Models;
using DocaSub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocaSub.Controllers
{
    public class SubRequestsController : Controller
    {
        private readonly DocaDbContext _dbContext;

        public SubRequestsController(DocaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("mes-demandes")]
        public IActionResult Index()
        {
            //_dbContext.Database.ExecuteSqlRaw("SELECT * .....");
            /*var liste = new List<SubRequest>
            {
                new SubRequest { Id = 1, Title = "Demande 1", Amount = 1000, Status = 1, CreatedAt=DateTime.Now },
                new SubRequest { Id = 2, Title = "Demande 2", Amount = 2000, Status = 2, CreatedAt=DateTime.Now },
                new SubRequest { Id = 3, Title = "Demande 3", Amount = 3000, Status = 3, CreatedAt=DateTime.Now }
            };*/
            /*var liste =  _dbContext.SubRequests.ToList();

            var liste2 = new List<SubRequest>
            {
                new SubRequest { Id = 1, Title = "Demande 5", Amount = 1000, Status = 1, CreatedAt=DateTime.Now },
                new SubRequest { Id = 2, Title = "Demande 6", Amount = 2000, Status = 2, CreatedAt=DateTime.Now },
                new SubRequest { Id = 3, Title = "Demande 7", Amount = 3000, Status = 3, CreatedAt=DateTime.Now }
            };*/

            var model = new SubRequestIndexViewModel
            {
                SubRequestActive = _dbContext.SubRequests.Where(x => x.Status != 0),
                SubRequesteOlder = from x in _dbContext.SubRequests
                                   where x.Status == 0
                                   select x
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Create")]
        public async Task<IActionResult> Create([FromForm]SubRequest subRequest)
        {
            if (ModelState.IsValid)
            {
                subRequest.CreatedAt = DateTime.Now;
                subRequest.Priority = 1;
                subRequest.Status = 1;
                _dbContext.SubRequests.Add(subRequest);
                await _dbContext.SaveChangesAsync();
                //ViewBag.SuccessMessage = "Demande enregistrée avec succès";
                TempData["SuccessMessage"] = "Demande enregistrée avec succès";
                //return View(subRequest);
                return RedirectToAction("Index");
            }
            return View(subRequest);
        }
    }
}
