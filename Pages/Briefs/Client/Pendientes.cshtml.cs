using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class PendientesModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;
       
        public IEnumerable<BriefVM>? BriefVM { get; set; }

        public IEnumerable<Brief>? Pendientes { get; set; }
        public IEnumerable<Brief>? DesignBriefs { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }





        public PendientesModel(UserManager<AppUser> userManager, IToastNotification notify, BriefService briefService)
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
                ViewData["WelcomeMessage"] = $"Bienvenid@, {user.FirstName}";

                // Recupera los "briefs" pendientes y filtra por el usuario actual
                Pendientes = await _briefService.GetBriefsPendientesAsync(user.Id);

                // Filtra los briefs que pertenecen al usuario actual
             
                Pendientes = Pendientes.Where(b => b.UserId == user.Id && b.CosteadorAsignado != "No Requiere").ToList();


            }
            else
            {
                _notify.AddErrorToastMessage("El usuario NO está autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            return Page();
        }
            


    }
}






