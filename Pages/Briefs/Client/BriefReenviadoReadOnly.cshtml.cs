using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class BriefReenviadoReadOnlyModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public List<Pieza>? PiezasAsociadas { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public BriefReenviadoReadOnlyModel(UserManager<AppUser> userManager, BriefService briefService, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _notify.AddErrorToastMessage("No se encontró el Usuario!!");
                return RedirectToAction("Create");
            }

            BriefDetails = await _briefService.GetBriefAsync(briefid);

            if (BriefDetails == null)
            {
                _notify.AddErrorToastMessage("No se encontró el brief!!");
                return RedirectToAction("Create");
            }

            PiezasAsociadas = await _briefService.GetPiezasAsync(briefid);

            return Page();
        }

        public async Task<IActionResult> OnGetDeletePiezaAsync(int briefid, int numPieza)
        {
            if (User.IsInRole("Asesor"))
            {
                // Obtén el brief por su ID
                var pieza = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

                // Verifica si se encontró el brief
                if (pieza != null)
                {
                    // Elimia la pieza en la base de datos
                    await _briefService.DeletePiezaAsync(pieza);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddErrorToastMessage("La pieza " + numPieza + " se eliminó");
                    return RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid });
                }
                else
                {
                    _notify.AddErrorToastMessage("No se encontró la pieza");
                    RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid });
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public IActionResult OnGetReenviar()
        {
            if (User.IsInRole("Asesor"))
            {
                // Redirige de vuelta a la página de riefReadOnly
                _notify.AddSuccessToastMessage("Cotización reenviada!!");
                return RedirectToPage("/Briefs/Client/RechazarDesign", new { briefid = BriefDetails!.BriefId });

            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }

        public IActionResult OnGetReenviarCostos(int briefid)
        {
            if (User.IsInRole("Asesor"))
            {
                // Redirige de vuelta a la página de riefReadOnly
                //_notify.AddSuccessToastMessage("Cotización reenviada!!");
                return RedirectToPage("/Briefs/Client/ReenviarCostos", new { briefid });

            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }

        public IActionResult OnGetReenviarDesign(int briefid)
        {
            if (User.IsInRole("Asesor"))
            {
                // Redirige de vuelta a la página de riefReadOnly
                //_notify.AddSuccessToastMessage("Cotización reenviada!!");
                return RedirectToPage("/Briefs/Client/ReenviarDesign", new { briefid });

            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }
    }
}
