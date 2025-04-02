using DocaSub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocaSub.Areas.Backoffice.Pages
{
    public class SubWithRequestModel : PageModel
    {
        public IEnumerable<Subvention> Subventions { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
