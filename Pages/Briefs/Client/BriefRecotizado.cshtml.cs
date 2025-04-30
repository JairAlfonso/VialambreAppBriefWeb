using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class BriefRecotizadoModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;

        public List<Pieza>? PiezasAsociadas { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public Brief? BriefOriginal { get; set; }

        [BindProperty]
        public Pieza? NumeroPieza { get; set; }

        public BriefRecotizadoModel(UserManager<AppUser> userManager, BriefService briefService, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid, int id)
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

            BriefOriginal = await _briefService.GetBriefAsync(id);

            if (BriefOriginal == null)
            {
                _notify.AddErrorToastMessage("No se encontró el brief!!");
                return RedirectToAction("Create");
            }

            PiezasAsociadas = await _briefService.GetPiezasAsync(id);

            if (PiezasAsociadas == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<Pieza> piezasAsociadas, int briefid, string consecutivo)
        {
            foreach (var pieza in piezasAsociadas)
            {
                // Comprueba si ya hay piezas asociadas a este Brief
                var piezas = await _briefService.GetPiezasByBriefIdAsync(briefid);

                if (piezas != null && piezas.Any())
                {
                    // Encuentra el número de pieza más alto entre las piezas existentes
                    var maxNumeroPieza = piezas.Max(p => p.NumeroPieza);

                    // Asigna el siguiente número de pieza
                    NumeroPieza = new Pieza { NumeroPieza = maxNumeroPieza + 1 };
                }
                else
                {
                    // Si no hay piezas, asigna NumeroPieza como 1
                    NumeroPieza = new Pieza { NumeroPieza = 1 };
                }

                var agregarComoNueva = Request.Form["AgregarComoNueva_" + pieza.Id];
                if (!string.IsNullOrEmpty(agregarComoNueva) && agregarComoNueva == "on")
                {
                    var nuevaPieza = new Pieza
                    {
                        BriefId = briefid,
                        Consecutivo = consecutivo,
                        PiezaName = pieza.PiezaName,
                        NumeroPieza = NumeroPieza.NumeroPieza,
                        CantidadPiezas = pieza.CantidadPiezas,
                        PiezaAlto = pieza.PiezaAlto,
                        PiezaAncho = pieza.PiezaAncho,
                        PiezaFondo = pieza.PiezaFondo,
                        MaterialInput = pieza.MaterialInput,
                        Tubo = pieza.Tubo,
                        Alambre = pieza.Alambre,
                        Lamina = pieza.Lamina,
                        Madera = pieza.Madera,
                        Formica = pieza.Formica,
                        Polietileno = pieza.Polietileno,
                        Acrilico = pieza.Acrilico,
                        Ps = pieza.Ps,
                        Pp = pieza.Pp,
                        Mdf = pieza.Mdf,
                        NingunMaterial = pieza.NingunMaterial,
                        Color = pieza.Color,

                        Publicidad = pieza.Publicidad,

                        Item1name = pieza.Item1name,
                        Item1Alto = pieza.Item1Alto,
                        Item1Ancho = pieza.Item1Ancho,

                        Item2name = pieza.Item2name,
                        Item2Alto = pieza.Item2Alto,
                        Item2Ancho = pieza.Item2Ancho,

                        Item3name = pieza.Item3name,
                        Item3Alto = pieza.Item3Alto,
                        Item3Ancho = pieza.Item3Ancho,

                        Item4name = pieza.Item4name,
                        Item4Alto = pieza.Item4Alto,
                        Item4Ancho = pieza.Item4Ancho,

                        Tintas = pieza.Tintas,
                        AcabadoInput = pieza.AcabadoInput,
                        Laminado = pieza.Laminado,
                        Refilado = pieza.Refilado,
                        Troquelado = pieza.Troquelado,
                        Termodoblado = pieza.Termodoblado,
                        Termoformado = pieza.Termoformado,
                        NingunAcabado = pieza.NingunAcabado,

                        Cargue = pieza.Cargue,
                        AcabadosyAccesoriosInput = pieza.AcabadosyAccesoriosInput,
                        Ruedas = pieza.Ruedas,
                        Niveladores = pieza.Niveladores,
                        Cromado = pieza.Cromado,
                        Zincado = pieza.Zincado,
                        ComentariosAdicionales = pieza.ComentariosAdicionales,
                        NingunAccesorio = pieza.NingunAccesorio,

                    };

                    if (pieza.Publicidad == false)
                    {
                        pieza.Item1name = "No requiere";
                        pieza.Item1Alto = "0";
                        pieza.Item1Ancho = "0";

                        pieza.Item2name = "No requiere";
                        pieza.Item2Alto = "0";
                        pieza.Item2Ancho = "0";

                        pieza.Item3name = "No requiere";
                        pieza.Item3Alto = "0";
                        pieza.Item3Ancho = "0";

                        pieza.Item4name = "No requiere";
                        pieza.Item4Alto = "0";
                        pieza.Item4Ancho = "0";
                    }


                    await _briefService.AddPiezaAsync(nuevaPieza);
                }
            }

            _notify.AddSuccessToastMessage("El brief " + consecutivo + " fue recotizado");
            return RedirectToPage("/Briefs/Client/BriefReadOnly", new { briefid = BriefDetails!.BriefId });
        }
    }
}
