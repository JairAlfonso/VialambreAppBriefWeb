using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class CancelarModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public CancelarModel(/*UserManager<AppUser> userManager,*/ BriefService briefService, IToastNotification notify)
        {
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            if (User.IsInRole("Asesor"))
            {
                BriefDetails = await _briefService.GetBriefAsync(briefid);
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEnviarAsync(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("Asesor"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            if (BriefVM!.RespuestaAsesorCostos != null)
            {
                brief.RespuestaAsesorCostos = BriefVM!.RespuestaAsesorCostos;
                brief.StatusDesign = BriefStatus.Cancelado;
                brief.StatusCostos = BriefStatus.Cancelado;
                brief.StatusAsesor = BriefStatus.Cancelado;
                brief.FechaRespuestaAsesorCostos = DateTime.Now;
            }
            else
            {
                _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                return RedirectToPage("/Briefs/Client/Cancelar", new { briefid = BriefDetails!.BriefId });
            }

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddErrorToastMessage("Cotización Cancelada!!");
            return RedirectToPage("/Briefs/Client/Details", new { id = BriefDetails!.BriefId });

        }
    }
}



/*public async Task<IActionResult> OnGetCancelarAsesorAsync(int briefid)
{
    // Obtener el brief con el ID especificado
    var brief = await _briefService.GetBriefAsync(briefid);



    // Cancelar el brief directamente sin confirmación
    brief.StatusDesign = BriefStatus.Cancelado;
    brief.StatusCostos = BriefStatus.Cancelado;
    brief.StatusAsesor = BriefStatus.Cancelado;

    // Actualizar el brief en la base de datos
    await _briefService.UpdateBriefAsync(brief);

    // Mostrar un mensaje de éxito temporal indicando que la cotización se ha cancelado
    TempData["Success"] = "Cotización cancelada!!";

    // Redireccionar a la página de "Pendientes" para el usuario actual, pasando el ID del brief cancelado
    return RedirectToPage("/Briefs/Client/Pendientes", new { id = brief.BriefId });

    // No hay bloque try...catch (asumir que UpdateBriefAsync maneja excepciones)

} */