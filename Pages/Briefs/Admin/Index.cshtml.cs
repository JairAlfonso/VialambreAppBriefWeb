using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using System.Security.Claims;

namespace VialambreAppTest1.Pages.Briefs.Admin
{
    public class IndexModel : PageModel
    {
        private readonly BriefService _briefService;

        public IEnumerable<Brief>? Briefs { get; set; }
        public int PendingBriefsCount { get; set; }
        public int ApprovedBriefsCount { get; set; }
        public int TotalCotizacionesCount { get; set; }
        public int RejectedBriefsCount { get; set; }
        public int DeliveredBriefsCount { get; set; }
        public int CancelledBriefsCount { get; set; }
        public int DesignApprovedBriefsCount { get; set; } // Nueva propiedad para contar briefs aprobados en diseño
        public int NuevaCotizacionCount { get; set; }
        public int LicitacionesCount { get; set; }

        public IndexModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/AccessDenied");
            }

            // Obtener briefs pendientes
            Briefs = await _briefService.GetBriefsPendientesAsync();
            PendingBriefsCount = Briefs.Count();

            // Contar todas las cotizaciones
            TotalCotizacionesCount = _briefService.ContarCotizaciones();

            // Obtener briefs aprobados
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtiene el UserId del usuario actual
            var approvedBriefs = await _briefService.GetBriefsAprobadosAsync(userId);
            ApprovedBriefsCount = approvedBriefs.Count();

            // Obtener briefs rechazados
            var rejectedBriefs = await _briefService.GetBriefsRechazadosAsync();
            RejectedBriefsCount = rejectedBriefs.Count();

            // Obtener briefs entregados
            var approved = await _briefService.GetBriefsAprobadosAsync();
            ApprovedBriefsCount = approved.Count;

            // Obtener briefs cancelados
            var cancelledBriefs = await _briefService.GetBriefsCanceladosAsync();
            CancelledBriefsCount = cancelledBriefs.Count();

            // Obtener briefs de diseño aprobados

            // Obtener briefs de nueva cotización
            var nuevaCotizaciones = await _briefService.GetNuevaCotizacionAsync();
            NuevaCotizacionCount = nuevaCotizaciones.Count;
            // Obtener briefs de licitaciones
            var licitaciones = await _briefService.GetLicitacionesAsync(userId);
            LicitacionesCount = licitaciones.Count;
            return Page();
        }
    }
}

