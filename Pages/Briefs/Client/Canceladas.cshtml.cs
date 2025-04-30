using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;
namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class CanceladasModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;
        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? Canceladas { get; set; }
        [BindProperty]
        public LoginVM? LoginVM { get; set; }       
        public CanceladasModel(UserManager<AppUser> userManager, IToastNotification notify, BriefService briefService)
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
                ViewData["WelcomeMessage"] = $"Bienvenido, {user.FullName}";
              // Recupera los "briefs" cancelados
                Canceladas = await _briefService.GetBriefsCanceladosAsync(user.Id);
                // Filtra los briefs que pertenecen al usuario actual
                Canceladas = Canceladas.Where(b => b.UserId == user.Id).ToList();
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