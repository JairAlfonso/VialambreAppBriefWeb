using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class AgregarPiezaReenviadaModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;
        private readonly IWebHostEnvironment _host;

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public PiezaVM? PiezaDetails { get; set; }

        [BindProperty]
        public Pieza? NumeroPieza { get; set; }

        [BindProperty]
        public Pieza? PiezaAnterior { get; set; }

        public AgregarPiezaReenviadaModel(BriefService briefService, IToastNotification notify, IWebHostEnvironment host)
        {
            _briefService = briefService;
            _notify = notify;
            _host = host;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            BriefDetails = await _briefService.GetBriefAsync(briefid);

            if (BriefDetails == null)
            {
                _notify.AddErrorToastMessage("No se encontró el brief!!");
                return RedirectToAction("Create");
            }

            // Comprueba si ya hay piezas asociadas a este Brief
            var piezas = await _briefService.GetPiezasByBriefIdAsync(briefid);

            if (piezas != null && piezas.Any())
            {
                // Encuentra el número de pieza más alto entre las piezas existentes
                var maxNumeroPieza = piezas.Max(p => p.NumeroPieza);

                // Asigna el siguiente número de pieza
                NumeroPieza = new Pieza { NumeroPieza = maxNumeroPieza + 1 };

                // Asigna el valor para la PiezaAnterior
                PiezaAnterior = new Pieza { NumeroPieza = maxNumeroPieza };
            }
            else
            {
                // Si no hay piezas, asigna NumeroPieza como 1
                NumeroPieza = new Pieza { NumeroPieza = 1 };


                // Si no hay piezas, asigna PiezaAnterior como 1
                PiezaAnterior = new Pieza { NumeroPieza = 1 };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAgregarPiezaAsync(int briefid)
        {
            //Este codigo guarda los archivos en carpeta local
            string webroot = _host.WebRootPath;
            var filePieza = PiezaDetails!.FilePieza; //HttpContext.Request.Form.Files["otrosarchivosInput"];
            string PiezaFolder = @"Images\ArchivosPieza";
            string UploadFolderPieza = Path.Combine(webroot, PiezaFolder);

            BriefDetails = await _briefService.GetBriefAsync(briefid);

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

            if (PiezaDetails != null)
            {
                var pieza = new Pieza
                {
                    BriefId = BriefDetails.BriefId,
                    Consecutivo = BriefDetails.Consecutivo,
                    PiezaName = PiezaDetails.PiezaName,
                    NumeroPieza = NumeroPieza!.NumeroPieza,
                    CantidadPiezas = PiezaDetails!.CantidadPiezas,

                    PiezaAlto = PiezaDetails.PiezaAlto,
                    PiezaAncho = PiezaDetails.PiezaAncho,
                    PiezaFondo = PiezaDetails.PiezaFondo,
                    MaterialInput = PiezaDetails.MaterialInput,
                    Tubo = PiezaDetails.Tubo,
                    Alambre = PiezaDetails.Alambre,
                    Lamina = PiezaDetails.Lamina,
                    Madera = PiezaDetails.Madera,
                    Formica = PiezaDetails.Formica,
                    Polietileno = PiezaDetails.Polietileno,
                    Acrilico = PiezaDetails.Acrilico,
                    Ps = PiezaDetails.Ps,
                    Pp = PiezaDetails.Pp,
                    Mdf = PiezaDetails.Mdf,
                    NingunMaterial = PiezaDetails.NingunMaterial,
                    Color = PiezaDetails.Color,

                    Publicidad = PiezaDetails.Publicidad,

                    Item1name = PiezaDetails.Item1name,
                    Item1Alto = PiezaDetails.Item1Alto,
                    Item1Ancho = PiezaDetails.Item1Ancho,

                    Item2name = PiezaDetails.Item2name,
                    Item2Alto = PiezaDetails.Item2Alto,
                    Item2Ancho = PiezaDetails.Item2Ancho,

                    Item3name = PiezaDetails.Item3name,
                    Item3Alto = PiezaDetails.Item3Alto,
                    Item3Ancho = PiezaDetails.Item3Ancho,

                    Item4name = PiezaDetails.Item4name,
                    Item4Alto = PiezaDetails.Item4Alto,
                    Item4Ancho = PiezaDetails.Item4Ancho,

                    AcabadoInput = PiezaDetails.AcabadoInput,
                    Laminado = PiezaDetails.Laminado,
                    Refilado = PiezaDetails.Refilado,
                    Troquelado = PiezaDetails.Troquelado,
                    Termodoblado = PiezaDetails.Termodoblado,
                    Termoformado = PiezaDetails.Termoformado,
                    NingunAcabado = PiezaDetails.NingunAcabado,

                    Cargue = PiezaDetails.Cargue,
                    AcabadosyAccesoriosInput = PiezaDetails.AcabadosyAccesoriosInput,
                    Ruedas = PiezaDetails.Ruedas,
                    Niveladores = PiezaDetails.Niveladores,
                    Cromado = PiezaDetails.Cromado,
                    Zincado = PiezaDetails.Zincado,
                    NingunAccesorio = PiezaDetails.NingunAccesorio,
                    ComentariosAdicionales = PiezaDetails.ComentariosAdicionales
                };

                if (PiezaDetails.Publicidad == false || PiezaDetails.Publicidad == null)
                {
                    pieza.Publicidad = false;

                    pieza.Item1name = "No reuqiere";
                    pieza.Item1Alto = "0";
                    pieza.Item1Ancho = "0";

                    pieza.Item2name = "No reuqiere";
                    pieza.Item2Alto = "0";
                    pieza.Item2Ancho = "0";

                    pieza.Item3name = "No reuqiere";
                    pieza.Item3Alto = "0";
                    pieza.Item3Ancho = "0";

                    pieza.Item4name = "No reuqiere";
                    pieza.Item4Alto = "0";
                    pieza.Item4Ancho = "0";

                    pieza.Tintas = "No requiere";
                    pieza.AcabadoInput = "No requiere";
                }

                if (PiezaDetails.ArchivoPieza != null && PiezaDetails.FilePieza != null)
                {
                    string fileNewName = await FileManager.CopyFilePieza(filePieza!, UploadFolderPieza);
                    pieza.ArchivoPieza = Path.Combine(PiezaFolder, fileNewName);
                }
                else if (PiezaDetails.ArchivoPieza == null && PiezaDetails.FilePieza == null)
                {
                    pieza.ArchivoPieza = null;
                }

                if (string.IsNullOrEmpty(PiezaDetails.PiezaName) || PiezaDetails.Cargue == null)
                {
                    _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                    RedirectToPage("/Briefs/Client/AgregarPiezaReenviada", new { briefid = BriefDetails!.BriefId });
                }
                else
                {
                    await _briefService.AddPiezaAsync(pieza);
                    _notify.AddSuccessToastMessage("La pieza " + pieza.NumeroPieza + " se añadió al brief " + @pieza.Consecutivo);
                    return RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = pieza.BriefId });
                }

            }
            else
            {
                _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                RedirectToPage("/Briefs/Client/AgregarPiezaReenviada", new { briefid = BriefDetails!.BriefId });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostFinalizarAsync(int briefid, int numPieza)
        {
            //Este codigo guarda los archivos en carpeta local
            string webroot = _host.WebRootPath;
            var filePieza = PiezaDetails!.FilePieza; //HttpContext.Request.Form.Files["otrosarchivosInput"];
            string PiezaFolder = @"Images\ArchivosPieza";
            string UploadFolderPieza = Path.Combine(webroot, PiezaFolder);

            BriefDetails = await _briefService.GetBriefAsync(briefid);

            // Comprueba si ya hay piezas asociadas a este Brief
            var piezas = await _briefService.GetPiezasByBriefIdAsync(briefid);

            if (piezas != null && piezas.Any())
            {
                // Encuentra el número de pieza más alto entre las piezas existentes
                var maxNumeroPieza = piezas.Max(p => p.NumeroPieza);

                // Asigna el siguiente número de pieza
                NumeroPieza = new Pieza { NumeroPieza = maxNumeroPieza + 1 };

                PiezaAnterior = new Pieza { NumeroPieza = numPieza};

            }
            else
            {
                // Si no hay piezas, asigna NumeroPieza como 1
                NumeroPieza = new Pieza { NumeroPieza = 1};

            }

            if (PiezaDetails != null)
            {
                var pieza = new Pieza
                {
                    BriefId = BriefDetails.BriefId,
                    Consecutivo = BriefDetails.Consecutivo,
                    PiezaName = PiezaDetails.PiezaName,
                    NumeroPieza = NumeroPieza.NumeroPieza,
                    CantidadPiezas = PiezaDetails!.CantidadPiezas,

                    PiezaAlto = PiezaDetails.PiezaAlto,
                    PiezaAncho = PiezaDetails.PiezaAncho,
                    PiezaFondo = PiezaDetails.PiezaFondo,
                    MaterialInput = PiezaDetails.MaterialInput,
                    Tubo = PiezaDetails.Tubo,
                    Alambre = PiezaDetails.Alambre,
                    Lamina = PiezaDetails.Lamina,
                    Madera = PiezaDetails.Madera,
                    Formica = PiezaDetails.Formica,
                    Polietileno = PiezaDetails.Polietileno,
                    Acrilico = PiezaDetails.Acrilico,
                    Ps = PiezaDetails.Ps,
                    Pp = PiezaDetails.Pp,
                    Mdf = PiezaDetails.Mdf,
                    NingunMaterial = PiezaDetails.NingunMaterial,
                    Color = PiezaDetails.Color,

                    Publicidad = PiezaDetails.Publicidad,

                    Item1name = PiezaDetails.Item1name,
                    Item1Alto = PiezaDetails.Item1Alto,
                    Item1Ancho = PiezaDetails.Item1Ancho,

                    Item2name = PiezaDetails.Item2name,
                    Item2Alto = PiezaDetails.Item2Alto,
                    Item2Ancho = PiezaDetails.Item2Ancho,

                    Item3name = PiezaDetails.Item3name,
                    Item3Alto = PiezaDetails.Item3Alto,
                    Item3Ancho = PiezaDetails.Item3Ancho,

                    Item4name = PiezaDetails.Item4name,
                    Item4Alto = PiezaDetails.Item4Alto,
                    Item4Ancho = PiezaDetails.Item4Ancho,

                    Tintas = PiezaDetails.Tintas,
                    AcabadoInput = PiezaDetails.AcabadoInput,
                    Laminado = PiezaDetails.Laminado,
                    Refilado = PiezaDetails.Refilado,
                    Troquelado = PiezaDetails.Troquelado,
                    Termodoblado = PiezaDetails.Termodoblado,
                    Termoformado = PiezaDetails.Termoformado,
                    NingunAcabado = PiezaDetails.NingunAcabado,

                    Cargue = PiezaDetails.Cargue,
                    AcabadosyAccesoriosInput = PiezaDetails.AcabadosyAccesoriosInput,
                    Ruedas = PiezaDetails.Ruedas,
                    Niveladores = PiezaDetails.Niveladores,
                    Cromado = PiezaDetails.Cromado,
                    Zincado = PiezaDetails.Zincado,
                    NingunAccesorio = PiezaDetails.NingunAccesorio,
                    ComentariosAdicionales = PiezaDetails.ComentariosAdicionales
                };

                if (PiezaDetails.Publicidad == false || PiezaDetails.Publicidad == null)
                {
                    pieza.Publicidad = false;

                    pieza.Item1name = "No reuqiere";
                    pieza.Item1Alto = "0";
                    pieza.Item1Ancho = "0";

                    pieza.Item2name = "No reuqiere";
                    pieza.Item2Alto = "0";
                    pieza.Item2Ancho = "0";

                    pieza.Item3name = "No reuqiere";
                    pieza.Item3Alto = "0";
                    pieza.Item3Ancho = "0";

                    pieza.Item4name = "No reuqiere";
                    pieza.Item4Alto = "0";
                    pieza.Item4Ancho = "0";

                    pieza.Tintas = "No requiere";
                    pieza.AcabadoInput = "No requiere";
                }

                if (PiezaDetails.ArchivoPieza != null && PiezaDetails.FilePieza != null)
                {
                    string fileNewName = await FileManager.CopyFilePieza(filePieza!, UploadFolderPieza);
                    pieza.ArchivoPieza = Path.Combine(PiezaFolder, fileNewName);
                }
                else if (PiezaDetails.ArchivoPieza == null && PiezaDetails.FilePieza == null)
                {
                    pieza.ArchivoPieza = null;
                }

                if (string.IsNullOrEmpty(PiezaDetails.PiezaName) || PiezaDetails.CantidadPiezas == null)
                {
                    _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                    return RedirectToPage("/Briefs/Client/AgregarPiezaReenviada");
                }
                else
                {
                    await _briefService.AddPiezaAsync(pieza);
                    _notify.AddSuccessToastMessage("Pieza " + pieza.NumeroPieza + " añadida al brief " + @pieza.Consecutivo);
                    return RedirectToPage("/Briefs/Client/Realizadas");
                }
            }
            else
            {
                _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                RedirectToPage("/Briefs/Client/AgregarPiezaReenviada", new { briefid = PiezaDetails!.BriefId });
            }

            return Page();
        }
    }
}
