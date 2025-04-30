using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Design
{
    public class PendientesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? DesignBriefs { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public PendientesModel(UserManager<AppUser> userManager, BriefService briefService, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.IsInRole("Design"))
            {
                DesignBriefs = await _briefService.GetDesignBriefsAsync();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetAsignar(int id)
        {
            if (User.IsInRole("Design"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(id);

                // Verifica si se encontró el brief
                if (brief != null)
                {
                    // Obtiene el usuario actualmente autenticado
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound(); // Si no se encuentra el usuario
                    }
                    else
                    {
                        // Cambia el estadoCostos del brief a "Realizado"
                        brief.AsignadoDesign = true;
                        brief.DesignerAsignado = user.FullName;
                        brief.FechaAsignacion = DateTime.Now;

                        // Guarda los cambios en la base de datos
                        await _briefService.UpdateBriefAsync(brief);
                    }

                    // Redirige de vuelta a la página de briefs asignadas
                    _notify.AddSuccessToastMessage("Brief " + brief.Consecutivo + " asignado a: " + brief.DesignerAsignado );
                    return RedirectToPage("/Briefs/Employee/Design/Asignadas");
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