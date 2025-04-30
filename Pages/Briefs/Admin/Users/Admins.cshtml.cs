using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Admin.Users
{
    public class AdminsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IEnumerable<AppUser>? Admins { get; set; }

        [BindProperty]
        public UserInputVM? UserVM { get; set; }

        public AdminsModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Admins = await _userManager.GetUsersInRoleAsync("Admin");
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}