using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class AprobadasModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;
        private readonly BriefService _briefService;

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? Aprobadas { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public AprobadasModel(UserManager<AppUser> userManager, IToastNotification notify, BriefService briefService)
        {
            _userManager = userManager;
            _notify = notify;
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["WelcomeMessage"] = $"Bienvenido, {user.FullName}!";
                // Recupera los "briefs" aprobados
                Aprobadas = await _briefService.GetBriefsAprobadosAsync(user.Id);
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