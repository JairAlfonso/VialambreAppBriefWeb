using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class EditarPiezaReenviadaModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IWebHostEnvironment _host;
        private readonly IToastNotification _notify;

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public PiezaVM? PiezaVM { get; set; }

        [BindProperty]
        public Pieza? PiezaDetails { get; set; }

        [BindProperty]
        public Pieza? NumeroPieza { get; set; }

        [BindProperty]
        public Pieza? PiezaAnterior { get; set; }

        public EditarPiezaReenviadaModel(UserManager<AppUser> userManager, BriefService briefService, IWebHostEnvironment host, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _host = host;
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int briefid, int numPieza)
        {
            _ = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Asesor"))
            {
                _notify.AddErrorToastMessage("El usuario NO esta autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            BriefDetails = await _briefService.GetBriefAsync(briefid);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que no se encontró la pieza
                return NotFound();
            }

            PiezaDetails = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

            if (PiezaDetails == null)
            {
                // Maneja el caso en el que no se encontró la pieza
                return NotFound();
            }

            NumeroPieza = new Pieza { NumeroPieza = numPieza };


            PiezaAnterior = new Pieza { NumeroPieza = (numPieza - 1)};


            PiezaVM = new PiezaVM
            {
                BriefId = briefid,
                NumeroPieza = numPieza,
                Consecutivo = BriefDetails.Consecutivo,
                PiezaName = PiezaDetails.PiezaName,
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
                    _notify.AddErrorToastMessage("Las piezas se eliminaron");
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

        public async Task<IActionResult> OnGetDeleteAndBackAsync(int briefid, int numPieza, int piezaAnt)
        {
            if (User.IsInRole("Asesor"))
            {
                // Obtiene la pieza por su ID
                var pieza = await _briefService.GetPiezaToDeleteAsync(briefid, numPieza);

                // Verifica si se encontró el brief
                if (pieza != null)
                {
                    // Elimia la pieza en la base de datos
                    await _briefService.DeletePiezaAsync(pieza);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddErrorToastMessage("La pieza " + numPieza + " se eliminó");
                    return RedirectToPage("/Briefs/Client/EditarPiezaReenviada", new { briefid, numPieza = piezaAnt });
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

        public async Task<IActionResult> OnPostFinalizarAsync(int briefid, int numPieza)
        {
            //Este codigo guarda los archivos en carpeta local
            string webroot = _host.WebRootPath;
            var filePieza = PiezaDetails!.FilePieza; //HttpContext.Request.Form.Files["otrosarchivosInput"];
            string PiezaFolder = @"Images\ArchivosPieza";
            string UploadFolderPieza = Path.Combine(webroot, PiezaFolder);

            BriefDetails = await _briefService.GetBriefAsync(briefid);

            // Comprueba si ya hay piezas asociadas a este Brief
            var piezaOriginal = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

            NumeroPieza = new Pieza { NumeroPieza = numPieza };


            if (piezaOriginal != null)
            {
                piezaOriginal.BriefId = piezaOriginal.BriefId;
                piezaOriginal.Consecutivo = piezaOriginal.Consecutivo;
                piezaOriginal.PiezaName = PiezaDetails!.PiezaName;
                piezaOriginal.NumeroPieza = NumeroPieza.NumeroPieza;
                piezaOriginal.CantidadPiezas = PiezaDetails.CantidadPiezas;
                piezaOriginal.PiezaAlto = PiezaDetails.PiezaAlto;
                piezaOriginal.PiezaAncho = PiezaDetails.PiezaAncho;
                piezaOriginal.PiezaFondo = PiezaDetails.PiezaFondo;
                piezaOriginal.MaterialInput = PiezaDetails.MaterialInput;
                piezaOriginal.Tubo = PiezaDetails.Tubo;
                piezaOriginal.Alambre = PiezaDetails.Alambre;
                piezaOriginal.Lamina = PiezaDetails.Lamina;
                piezaOriginal.Madera = PiezaDetails.Madera;
                piezaOriginal.Formica = PiezaDetails.Formica;
                piezaOriginal.Polietileno = PiezaDetails.Polietileno;
                piezaOriginal.Acrilico = PiezaDetails.Acrilico;
                piezaOriginal.Ps = PiezaDetails.Ps;
                piezaOriginal.Pp = PiezaDetails.Pp;
                piezaOriginal.Mdf = PiezaDetails.Mdf;
                piezaOriginal.NingunMaterial = PiezaDetails.NingunMaterial;
                piezaOriginal.Color = PiezaDetails.Color;

                piezaOriginal.Publicidad = PiezaDetails.Publicidad;

                piezaOriginal.Item1name = PiezaDetails.Item1name;
                piezaOriginal.Item1Alto = PiezaDetails.Item1Alto;
                piezaOriginal.Item1Ancho = PiezaDetails.Item1Ancho;

                piezaOriginal.Item2name = PiezaDetails.Item2name;
                piezaOriginal.Item2Alto = PiezaDetails.Item2Alto;
                piezaOriginal.Item2Ancho = PiezaDetails.Item2Ancho;

                piezaOriginal.Item3name = PiezaDetails.Item3name;
                piezaOriginal.Item3Alto = PiezaDetails.Item3Alto;
                piezaOriginal.Item3Ancho = PiezaDetails.Item3Ancho;

                piezaOriginal.Item4name = PiezaDetails.Item4name;
                piezaOriginal.Item4Alto = PiezaDetails.Item4Alto;
                piezaOriginal.Item4Ancho = PiezaDetails.Item4Ancho;

                piezaOriginal.Tintas = PiezaDetails.Tintas;
                piezaOriginal.AcabadoInput = PiezaDetails.AcabadoInput;
                piezaOriginal.Laminado = PiezaDetails.Laminado;
                piezaOriginal.Refilado = PiezaDetails.Refilado;
                piezaOriginal.Troquelado = PiezaDetails.Troquelado;
                piezaOriginal.Termodoblado = PiezaDetails.Termodoblado;
                piezaOriginal.Termoformado = PiezaDetails.Termoformado;
                piezaOriginal.NingunAcabado = PiezaDetails.NingunAcabado;

                piezaOriginal.Cargue = PiezaDetails.Cargue;
                piezaOriginal.AcabadosyAccesoriosInput = PiezaDetails.AcabadosyAccesoriosInput;
                piezaOriginal.Ruedas = PiezaDetails.Ruedas;
                piezaOriginal.Niveladores = PiezaDetails.Niveladores;
                piezaOriginal.Cromado = PiezaDetails.Cromado;
                piezaOriginal.Zincado = PiezaDetails.Zincado;
                piezaOriginal.NingunAccesorio = PiezaDetails.NingunAccesorio;

                piezaOriginal.ComentariosAdicionales = PiezaDetails.ComentariosAdicionales;

                if (PiezaDetails.Publicidad == false || PiezaDetails.Publicidad == null)
                {
                    piezaOriginal.Publicidad = false;

                    piezaOriginal.Item1name = "No reuqiere";
                    piezaOriginal.Item1Alto = "0";
                    piezaOriginal.Item1Ancho = "0";

                    piezaOriginal.Item2name = "No reuqiere";
                    piezaOriginal.Item2Alto = "0";
                    piezaOriginal.Item2Ancho = "0";

                    piezaOriginal.Item3name = "No reuqiere";
                    piezaOriginal.Item3Alto = "0";
                    piezaOriginal.Item3Ancho = "0";

                    piezaOriginal.Item4name = "No reuqiere";
                    piezaOriginal.Item4Alto = "0";
                    piezaOriginal.Item4Ancho = "0";

                    piezaOriginal.Tintas = "No requiere";
                    piezaOriginal.AcabadoInput = "No requiere";
                }

                if (PiezaDetails.ArchivoPieza != null && PiezaDetails.FilePieza != null)
                {
                    string fileNewName = await FileManager.CopyFilePieza(filePieza!, UploadFolderPieza);
                    piezaOriginal.ArchivoPieza = Path.Combine(PiezaFolder, fileNewName);
                }
                else if (PiezaDetails.ArchivoPieza == null && PiezaDetails.FilePieza == null)
                {
                    piezaOriginal.ArchivoPieza = null;
                }

                if (string.IsNullOrEmpty(PiezaDetails.PiezaName) || PiezaDetails.CantidadPiezas == null)
                {
                    _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                    RedirectToPage("/Briefs/Client/EditarPiezaReenviada", new { briefid = BriefDetails.BriefId, numPieza = NumeroPieza!.NumeroPieza });
                }
                else
                {
                    await _briefService.UpdatePiezaAsync(piezaOriginal);
                    _notify.AddSuccessToastMessage("Pieza " + piezaOriginal.NumeroPieza + " añadida al brief " + piezaOriginal.Consecutivo);
                    return RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = BriefDetails!.BriefId });
                }
            }
            else
            {
                _notify.AddErrorToastMessage("No hay piezas asociadas");
                RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = BriefDetails!.BriefId });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAgregarPiezaAsync(int briefid, int numPieza)
        {
            //Este codigo guarda los archivos en carpeta local
            string webroot = _host.WebRootPath;
            var filePieza = PiezaDetails!.FilePieza; //HttpContext.Request.Form.Files["otrosarchivosInput"];
            string PiezaFolder = @"Images\ArchivosPieza";
            string UploadFolderPieza = Path.Combine(webroot, PiezaFolder);

            BriefDetails = await _briefService.GetBriefAsync(briefid);

            // Comprueba si ya hay piezas asociadas a este Brief
            var piezaOriginal = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

            NumeroPieza = new Pieza { NumeroPieza = numPieza};


            if (piezaOriginal != null)
            {
                piezaOriginal.BriefId = piezaOriginal.BriefId;
                piezaOriginal.Consecutivo = piezaOriginal.Consecutivo;
                piezaOriginal.PiezaName = PiezaDetails!.PiezaName;
                piezaOriginal.NumeroPieza = NumeroPieza.NumeroPieza;
                piezaOriginal.CantidadPiezas = PiezaDetails.CantidadPiezas;
                piezaOriginal.PiezaAlto = PiezaDetails.PiezaAlto;
                piezaOriginal.PiezaAncho = PiezaDetails.PiezaAncho;
                piezaOriginal.PiezaFondo = PiezaDetails.PiezaFondo;
                piezaOriginal.MaterialInput = PiezaDetails.MaterialInput;
                piezaOriginal.Tubo = PiezaDetails.Tubo;
                piezaOriginal.Alambre = PiezaDetails.Alambre;
                piezaOriginal.Lamina = PiezaDetails.Lamina;
                piezaOriginal.Madera = PiezaDetails.Madera;
                piezaOriginal.Formica = PiezaDetails.Formica;
                piezaOriginal.Polietileno = PiezaDetails.Polietileno;
                piezaOriginal.Acrilico = PiezaDetails.Acrilico;
                piezaOriginal.Ps = PiezaDetails.Ps;
                piezaOriginal.Pp = PiezaDetails.Pp;
                piezaOriginal.Mdf = PiezaDetails.Mdf;
                piezaOriginal.NingunMaterial = PiezaDetails.NingunMaterial;
                piezaOriginal.Color = PiezaDetails.Color;

                piezaOriginal.Publicidad = PiezaDetails.Publicidad;

                piezaOriginal.Item1name = PiezaDetails.Item1name;
                piezaOriginal.Item1Alto = PiezaDetails.Item1Alto;
                piezaOriginal.Item1Ancho = PiezaDetails.Item1Ancho;

                piezaOriginal.Item2name = PiezaDetails.Item2name;
                piezaOriginal.Item2Alto = PiezaDetails.Item2Alto;
                piezaOriginal.Item2Ancho = PiezaDetails.Item2Ancho;

                piezaOriginal.Item3name = PiezaDetails.Item3name;
                piezaOriginal.Item3Alto = PiezaDetails.Item3Alto;
                piezaOriginal.Item3Ancho = PiezaDetails.Item3Ancho;

                piezaOriginal.Item4name = PiezaDetails.Item4name;
                piezaOriginal.Item4Alto = PiezaDetails.Item4Alto;
                piezaOriginal.Item4Ancho = PiezaDetails.Item4Ancho;

                piezaOriginal.Tintas = PiezaDetails.Tintas;
                piezaOriginal.AcabadoInput = PiezaDetails.AcabadoInput;
                piezaOriginal.Laminado = PiezaDetails.Laminado;
                piezaOriginal.Refilado = PiezaDetails.Refilado;
                piezaOriginal.Troquelado = PiezaDetails.Troquelado;
                piezaOriginal.Termodoblado = PiezaDetails.Termodoblado;
                piezaOriginal.Termoformado = PiezaDetails.Termoformado;
                piezaOriginal.NingunAcabado = PiezaDetails.NingunAcabado;

                piezaOriginal.Cargue = PiezaDetails.Cargue;
                piezaOriginal.AcabadosyAccesoriosInput = PiezaDetails.AcabadosyAccesoriosInput;
                piezaOriginal.Ruedas = PiezaDetails.Ruedas;
                piezaOriginal.Niveladores = PiezaDetails.Niveladores;
                piezaOriginal.Cromado = PiezaDetails.Cromado;
                piezaOriginal.Zincado = PiezaDetails.Zincado;
                piezaOriginal.NingunAccesorio = PiezaDetails.NingunAccesorio;
                piezaOriginal.ComentariosAdicionales = PiezaDetails.ComentariosAdicionales;

                if (PiezaDetails.Publicidad == false || PiezaDetails.Publicidad == null)
                {
                    piezaOriginal.Publicidad = false;

                    piezaOriginal.Item1name = "No requiere";
                    piezaOriginal.Item1Alto = "0";
                    piezaOriginal.Item1Ancho = "0";

                    piezaOriginal.Item2name = "No requiere";
                    piezaOriginal.Item2Alto = "0";
                    piezaOriginal.Item2Ancho = "0";

                    piezaOriginal.Item3name = "No requiere";
                    piezaOriginal.Item3Alto = "0";
                    piezaOriginal.Item3Ancho = "0";

                    piezaOriginal.Item4name = "No requiere";
                    piezaOriginal.Item4Alto = "0";
                    piezaOriginal.Item4Ancho = "0";

                    piezaOriginal.Tintas = "No requiere";
                    piezaOriginal.AcabadoInput = "No requiere";
                }

                if (PiezaDetails.ArchivoPieza == null && PiezaDetails.FilePieza != null)
                {
                    string fileNewName = await FileManager.CopyFilePieza(filePieza!, UploadFolderPieza);
                    piezaOriginal.ArchivoPieza = Path.Combine(PiezaFolder, fileNewName);
                }
                else if (PiezaDetails.ArchivoPieza == null && PiezaDetails.FilePieza == null)
                {
                    piezaOriginal.ArchivoPieza = null;
                }

                if (string.IsNullOrEmpty(PiezaDetails.PiezaName) || PiezaDetails.CantidadPiezas == null)
                {
                    _notify.AddErrorToastMessage("La pieza no puede estar vacía");
                    RedirectToPage("/Briefs/Client/EditarPiezaReenviada", new { briefid = BriefDetails.BriefId, numPieza = NumeroPieza!.NumeroPieza });
                }
                else
                {
                    await _briefService.UpdatePiezaAsync(piezaOriginal);
                    _notify.AddSuccessToastMessage("Pieza " + piezaOriginal.NumeroPieza + " añadida al brief " + piezaOriginal.Consecutivo);
                    return RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = BriefDetails!.BriefId });
                }
            }
            else
            {
                _notify.AddErrorToastMessage("No hay piezas asociadas");
                RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = BriefDetails!.BriefId });
            }

            return Page();
        }
    }
}
