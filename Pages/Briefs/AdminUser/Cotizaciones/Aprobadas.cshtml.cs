using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.AdminUser.Cotizaciones
{
    public class AprobadasModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? Briefs { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public AprobadasModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/AccessDenied");
            }

            await OnGetBriefsAprobados();
            return Page();
        }

        public async Task<IActionResult> OnGetBriefsAprobados()
        {
            if (User.IsInRole("AdminUser"))
            {
                Briefs = await _briefService.GetBriefsAprobadosAsync();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}