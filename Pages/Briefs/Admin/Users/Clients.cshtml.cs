using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Admin.Users
{
    public class ClientsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IEnumerable<AppUser>? Clients { get; set; }

        [BindProperty]
        public UserInputVM? UserVM { get; set; }

        public ClientsModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Clients = await _userManager.GetUsersInRoleAsync("Asesor");
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}