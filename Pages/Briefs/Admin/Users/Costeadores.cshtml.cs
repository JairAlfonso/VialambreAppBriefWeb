using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Admin.Users
{
    public class CosteadoresModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IEnumerable<AppUser>? Costeadores { get; set; }

        [BindProperty]
        public UserInputVM? UserVM { get; set; }

        public CosteadoresModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Costeadores = await _userManager.GetUsersInRoleAsync("Costos");
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}