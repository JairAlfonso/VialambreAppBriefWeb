using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class RechazarModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public RechazarModel(/*UserManager<AppUser> userManager,*/ BriefService briefService, IToastNotification notify)
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

        public async Task<IActionResult> OnPostRechazar(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("Costos"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            if (BriefVM!.RespuestaCostos != null)
            {
                brief.StatusCostos = BriefStatus.Rechazado;
                brief.RespuestaCostos = BriefVM!.RespuestaCostos;
                brief.FechaRespuestaCostos = DateTime.Now;
            }
            else
            {
                _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                return RedirectToPage("/Briefs/Employee/Costos/Rechazar", new { briefid = BriefDetails!.BriefId });
            }

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddErrorToastMessage("Solicitud rechazada!!");
            return RedirectToPage("/Briefs/Employee/Costos/Details", new { id = BriefDetails!.BriefId });

        }
    }
}
