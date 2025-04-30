using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Admin.Users
{
    public class DesignersModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IEnumerable<AppUser>? Designers { get; set; }

        [BindProperty]
        public UserInputVM? UserVM { get; set; }

        public DesignersModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Designers = await _userManager.GetUsersInRoleAsync("Design");
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}
