using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.AdminUser.Cotizaciones
{
    public class EntregadasModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? Briefs { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public EntregadasModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/AccessDenied");
            }

            await OnGetBriefsEntregados();
            return Page();
        }

        public async Task<IActionResult> OnGetBriefsEntregados()
        {
            if (User.IsInRole("AdminUser"))
            {
                Briefs = await _briefService.GetBriefsEntregadosAsync();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}
