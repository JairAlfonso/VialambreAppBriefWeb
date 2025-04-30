using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class ReasignarModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public ReasignarModel(/*UserManager<AppUser> userManager,*/ BriefService briefService, IToastNotification notify)
        {
            //_userManager = userManager;
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            if (User.IsInRole("Costos"))
            {
                BriefDetails = await _briefService.GetBriefAsync(briefid);
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostReasignar(int briefId)
        {
            try
            {
                var brief = await _briefService.GetBriefAsync(briefId);

                if (brief == null)
                {
                    _notify.AddErrorToastMessage("El brief no existe.");
                    return NotFound();
                }

                if (!User.IsInRole("Costos"))
                {
                    return RedirectToPage("/Users/AccessDenied");
                }

                if (string.IsNullOrEmpty(BriefVM?.RespuestaCostos))
                {
                    _notify.AddErrorToastMessage("La respuesta no puede estar vacía.");
                    return RedirectToPage("/Briefs/Employee/Costos/Reasignar", new { briefId });
                }

                if (!string.IsNullOrWhiteSpace(BriefVM.RespuestaCostos))
                {
                    brief.RespuestaCostos = BriefVM.RespuestaCostos;
                }

                brief.StatusCostos = BriefStatus.Pendiente;
                brief.AsignadoCostos = false; // AsignadoCostos se establece siempre en false, ya que no se utiliza la variable "numero"
                brief.CosteadorAsignado = "Sin asignar";
                brief.FechaRespuestaCostos = DateTime.Now;

                await _briefService.UpdateBriefAsync(brief);

                _notify.AddSuccessToastMessage("Solicitud reasignada correctamente.");
                return RedirectToPage("/Briefs/Employee/Costos/Details", new { id = brief.BriefId });
            }
            catch (Exception ex)
            {
                _notify.AddErrorToastMessage($"Error al procesar la solicitud: {ex.Message}");
                return Page(); // Otra acción que consideres apropiada en caso de error.
            }
        }
    }
}

