using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Admin.Cotizaciones
{
    public class PendientesModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? Briefs { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public PendientesModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/AccessDenied");
            }

            await OnGetBriefsPendientes();
            return Page();
        }

        public async Task<IActionResult> OnGetBriefsPendientes()
        {
            if (User.IsInRole("Admin"))
            {
                Briefs = await _briefService.GetBriefsPendientesAsync();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}