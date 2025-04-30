using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class RechazadasModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? BriefsCostos { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public RechazadasModel(UserManager<AppUser> userManager, BriefService briefService)
        {
            _userManager = userManager;
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Costos"))
            {
                BriefsCostos = await _briefService.GetBriefsRechazadosCostosAsync(user.FullName);
                return Page();
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }
    }
}