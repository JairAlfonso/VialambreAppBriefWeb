using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Design
{
    public class EntregadasModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? BriefsCostos { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public EntregadasModel(UserManager<AppUser> userManager, BriefService briefService)
        {
            _userManager = userManager;
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Design"))
            {
                BriefsCostos = await _briefService.GetDesignEntregadoAsync(user.FullName);
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}
