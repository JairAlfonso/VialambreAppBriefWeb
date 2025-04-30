using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;


namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class PendientesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? CostosBriefs { get; set; }


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
            if (User.IsInRole("Costos"))
            {
                var todosLosBriefs = await _briefService.GetCostosBriefsAsync();
                CostosBriefs = todosLosBriefs.Where(brief => brief.CosteadorAsignado != "No Requiere").ToList();

            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetAsignar(int id)
        {
            if (User.IsInRole("Costos"))
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
                        // Cambia el campo AsigandoCostos a "Asignado"
                        brief.AsignadoCostos = true;
                        brief.CosteadorAsignado = user.FullName;

                        // Guarda los cambios en la base de datos
                        await _briefService.UpdateBriefAsync(brief);
                    }

                    // Redirige de vuelta a la página de briefs asiganadas
                    _notify.AddSuccessToastMessage("Brief " + brief.Consecutivo + " asignado a: " + brief.CosteadorAsignado);
                    return RedirectToPage("/Briefs/Employee/Costos/Asignadas");
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








        public class ReasignadasModel : PageModel
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly BriefService _briefService;
       
            public IEnumerable<BriefVM>? BriefVM { get; set; }
            public IEnumerable<Brief>? BriefsCostos { get; set; }
           
            [BindProperty]
            public LoginVM? LoginVM { get; set; }


            public ReasignadasModel(UserManager<AppUser> userManager, BriefService briefService)

            {
                _userManager = userManager;
                _briefService = briefService;
            }

            public async Task<IActionResult> OnGet()
            {
                var user = await _userManager.GetUserAsync(User);
               

                if (User.IsInRole("Costos"))
                {
                    BriefsCostos = await _briefService.GetBriefsReasignadosCostosAsync(user.FullName);
                    
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Users/AccessDenied");
                }
            }


        }
    }
}


