using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class BriefReadOnlyModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;
        private readonly IWebHostEnvironment _host;

        public List<Pieza>? PiezasAsociadas { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public BriefReadOnlyModel(UserManager<AppUser> userManager, BriefService briefService, IToastNotification notify, IWebHostEnvironment host)
        {
            _userManager = userManager;
            _briefService = briefService;
            _notify = notify;
            _host = host;
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
            //Activar para guardar archivos en local
            string webroot = _host.WebRootPath;

            if (User.IsInRole("Asesor"))
            {
                // Obtén la pieza por su ID
                var archivopieza = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

                // Verifica si se encontró el brief
                if (archivopieza != null && !string.IsNullOrEmpty(archivopieza.ArchivoPieza))
                {
                    //// Código para eliminar la imagen antigua del bucket aws
                    //var credentials = new BasicAWSCredentials("AKIAVQ624AHFXLQI2TUU", "N4oIHlCI/tO753kpg+bXi0+xYWPEREJhVuKxQ4V3");
                    //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                    //// Obtiene el nombre del objeto de la URL
                    //var objectKey = brief.ImageUrl.Split('/').Last();

                    //var deleteRequest = new DeleteObjectRequest
                    //{
                    //    BucketName = "imagenesvialambre",
                    //    Key = objectKey
                    //};

                    //await s3Client.DeleteObjectAsync(deleteRequest);

                    //Codigo para eliminar el archivo local
                    string filePath = Path.Combine(webroot, archivopieza.ArchivoPieza);
                    FileManager.DeletefilePieza(filePath);

                    // Cambia el campo AsigandoCostos a "Asignado"
                    archivopieza.ArchivoPieza = null;

                }

                // Obtén el brief por su ID
                var pieza = await _briefService.GetPiezaToEditAsync(briefid, numPieza);

                // Verifica si se encontró el brief
                if (pieza != null)
                {
                    // Elimia la pieza en la base de datos
                    await _briefService.DeletePiezaAsync(pieza);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddErrorToastMessage("La pieza " + numPieza + " se eliminó");
                    return RedirectToPage("/Briefs/Client/BriefReadOnly", new { briefid });
                }
                else
                {
                    _notify.AddErrorToastMessage("No se encontró la pieza");
                    RedirectToPage("/Briefs/Client/BriefReadOnly", new { briefid });
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            return Page();
        }

        public IActionResult OnGetEnviar(string consec)
        {
            if (User.IsInRole("Asesor"))
            {
                // Redirige de vuelta a la página de riefReadOnly
                _notify.AddSuccessToastMessage( consec + " enviado!!");
                return RedirectToPage("/Briefs/Client/Pendientes");

            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }
    }
}
