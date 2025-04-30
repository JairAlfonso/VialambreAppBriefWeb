using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.Pages.Briefs.AdminUser.Cotizaciones
{
    public class LicitacionesModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<Brief>? Licitaciones { get; set; }

        public LicitacionesModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public void OnGet()
        {

        }
    }
}
