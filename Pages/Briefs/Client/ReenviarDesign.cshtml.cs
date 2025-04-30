using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class ReenviarDesignModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        public ReenviarDesignModel(/*UserManager<AppUser> userManager,*/ BriefService briefService, IToastNotification notify)
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

        public async Task<IActionResult> OnPostAsync()
        {

            var brief = await _briefService.GetBriefAsync(BriefDetails!.BriefId);

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

            brief.RespuestaAsesorDesign = BriefVM!.RespuestaAsesorDesign;
            brief.StatusDesign = BriefStatus.Pendiente;
            brief.FechaRespuestaAsesorDesign = DateTime.Now;

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddSuccessToastMessage("Cotización reenviada!!");
            return RedirectToPage("/Briefs/Client/Details", new { id = BriefDetails!.BriefId });
        }

    }
}
