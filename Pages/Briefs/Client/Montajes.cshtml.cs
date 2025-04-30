using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class MontajesModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? Montajes { get; set; }

        public MontajesModel(BriefService briefService, UserManager<AppUser> userManager, IToastNotification notify)
        {
            _briefService = briefService;
            _userManager = userManager;
            _notify = notify;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["WelcomeMessage"] = $"Bienvenido, {user.FullName}!";
                // Recupera los "briefs" asociados a este usuario por su UserId
                Montajes = await _briefService.GetMontajesAsync(user.Id);
            }
            else
            {
                _notify.AddErrorToastMessage("El usuario NO esta autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            return Page();
        }
    }
}