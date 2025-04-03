using DocaSub.Data;
using DocaSub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DocaSub.ViewComponents
{
    public class MenuSubRequest : ViewComponent
    {
        private readonly DocaDbContext _dbContext;
        public MenuSubRequest(DocaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new MenuSubRequestViewModel();
            vm.RequestCount = _dbContext.SubRequests.Count(x => x.Status != 0);
            return View(vm);
        }
    }

    
}
