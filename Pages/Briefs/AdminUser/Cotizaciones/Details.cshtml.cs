using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.AdminUser.Cotizaciones
{
    public class DetailsModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public List<Pieza>? PiezasAsociadas { get; set; }

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? DesignBriefs { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        [BindProperty]
        public PiezaVM? PiezaVM { get; set; }

        public DetailsModel(BriefService briefService, IToastNotification notify)
        {
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            BriefDetails = await _briefService.GetBriefDetailsAsync(id);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                _notify.AddErrorToastMessage("No se encontró el brief!!");
                return RedirectToPage("/Briefs/AdminUser/Cotizaciones/Realizadas");
            }

            PiezasAsociadas = await _briefService.GetPiezasAsync(id);

            if (PiezasAsociadas == null)
            {
                // Maneja el caso en el que no hay piezas
                //_notify.AddErrorToastMessage("No se encontrarón piezas!!");
                return RedirectToPage("/Briefs/AdminUser/Cotizaciones/BriefReadOnly", new { briefid = BriefDetails.BriefId });
            }

            return Page();
        }


        public async Task<IActionResult> OnGetAprobarDesignAsync(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            brief.StatusDesign = BriefStatus.Aceptado;
            brief.FechaDesignAceptado = DateTime.Now;

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddSuccessToastMessage("Diseño aceptado!!");
            return RedirectToPage("/Briefs/AdminUser/Cotizaciones/Details", new { id = brief.BriefId });
        }

        public async Task<IActionResult> OnGetRechazarDesignAsync(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            brief.StatusDesign = BriefStatus.Pendiente;
            brief.FechaRespuestaDesign = DateTime.Now;

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddErrorToastMessage("El diseño fue rechazado!!");
            return RedirectToPage("/Briefs/AdminUser/Cotizaciones/Details", new { id = brief.BriefId });
        }

        public async Task<IActionResult> OnGetAprobarCostosAsync(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            brief.StatusCostos = BriefStatus.Aceptado;
            brief.FechaCostosAceptado = DateTime.Now;

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddSuccessToastMessage("Cotización aceptada!!");
            return RedirectToPage("/Briefs/AdminUser/Cotizaciones/Details", new { id = brief.BriefId });
        }

        public async Task<IActionResult> OnGetRechazarCostosAsync(int briefid)
        {
            var brief = await _briefService.GetBriefAsync(briefid);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("El brief no existe...");
                return NotFound();
            }

            if (!User.IsInRole("AdminUser"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            brief.StatusCostos = BriefStatus.Pendiente;
            brief.FechaRespuestaCostos = DateTime.Now;

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddErrorToastMessage("La cotización fue rechazada!!");
            return RedirectToPage("/Briefs/AdminUser/Cotizaciones/Details", new { id = brief.BriefId });
        }

    }
}