using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.AdminUser.Users
{
    public class AdminUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IEnumerable<AppUser>? AdminUser { get; set; }

        [BindProperty]
        public UserInputVM? UserVM { get; set; }

        public AdminUserModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("AdminUser"))
            {
                AdminUser = await _userManager.GetUsersInRoleAsync("AdminUser");
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}