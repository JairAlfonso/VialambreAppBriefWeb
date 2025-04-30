using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;


namespace VialambreAppTest1.Pages.Briefs.Admin.Cotizaciones
{
    public class RecotizacionesModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<Brief>? BriefsRecotizados { get; set; }
        public RecotizacionesModel(BriefService briefService)

        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/AccessDenied");
            }

            await OnGetBriefsRecotizados();
            return Page();
        }

        public async Task<IActionResult> OnGetBriefsRecotizados()
        {
            if (User.IsInRole("Admin"))
            {
                BriefsRecotizados = await _briefService.GetBriefsRecotizadosAsync();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}