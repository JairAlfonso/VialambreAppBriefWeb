using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Design
{
    public class EditarRespuestaModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public EditarRespuestaModel(BriefService briefService, IToastNotification notify)
        {
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            if (User.IsInRole("Design"))
            {
                BriefDetails = await _briefService.GetBriefAsync(briefid);
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var brief = await _briefService.GetBriefAsync(BriefDetails!.BriefId);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("Design"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            if (BriefVM!.RespuestaDesign != null)
            {
                brief.RespuestaDesign = BriefVM!.RespuestaDesign;
                brief.StatusDesign = BriefStatus.Entregado;
                brief.FechaRespuestaDesign = DateTime.Now;
                await _briefService.UpdateBriefAsync(brief);
            }
            else
            {
                _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                return RedirectToPage("/Briefs/Employee/Design/EditarRespuesta", new { briefid = BriefDetails!.BriefId });
            }

            _notify.AddSuccessToastMessage("La respuesta fue registrada!!");
            return RedirectToPage("/Briefs/Employee/Design/Entregadas");

        }
    }
}
