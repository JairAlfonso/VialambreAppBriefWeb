using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Design
{
    public class AsignadasModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? BriefAsignadosDesign { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public AsignadasModel(UserManager<AppUser> userManager, BriefService briefService, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Design"))
            {
                BriefAsignadosDesign = await _briefService.GetBriefsAsignadosDesignAsync(user.FullName);
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetRechazar(int id)
        {
            if (User.IsInRole("Design"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(id);

                // Verifica si se encontró el brief
                if (brief != null)
                {
                    // Cambia el estado del brief a "Realizado"
                    brief.StatusDesign = BriefStatus.Rechazado;
                    brief.FechaRespuestaDesign = DateTime.Now;

                    // Guarda los cambios en la base de datos
                    await _briefService.UpdateBriefAsync(brief);

                    // Redirige de vuelta a la página de briefs pendientes
                    _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                    return RedirectToPage("/Briefs/Employee/Design/Rechazadas");
                }
                else
                {
                    return NotFound(); // Si no se encuentra el brief
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }
    }
}
