using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class DetailsModel : PageModel
    {
        private readonly BriefService _briefService;
        private readonly IWebHostEnvironment _host;

        public List<Pieza>? PiezasAsociadas { get; set; }

        public IEnumerable<BriefVM>? BriefsList { get; set; }
        public IEnumerable<Brief>? CostosBriefs { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public DetailsModel(BriefService briefService, IWebHostEnvironment host)
        {
            _briefService = briefService;
            _host = host;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (!User.IsInRole("Costos"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            BriefDetails = await _briefService.GetBriefDetailsAsync(id);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                return NotFound();
            }

            PiezasAsociadas = await _briefService.GetPiezasAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnGetEditarRespuesta(int briefid)
        {
            //Activar para guardar archivos en local
            string webroot = _host.WebRootPath;

            if (User.IsInRole("Costos"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(briefid);

                // Verifica si se encontró el brief
                if (brief != null && brief.CosteadorAsignado != "No Requiere" && !string.IsNullOrEmpty(brief.RespuestaCostosInput))
                {
                    //// Código para eliminar el archivo antigua
                    //var credentials = new BasicAWSCredentials("AKIAVQ624AHFXLQI2TUU", "N4oIHlCI/tO753kpg+bXi0+xYWPEREJhVuKxQ4V3");
                    //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                    //// Obtiene el nombre del objeto de la URL
                    //var objectKey = brief.RespuestaCostosInput.Split('/').Last();

                    //var deleteRequest = new DeleteObjectRequest
                    //{
                    //    BucketName = "imagenesvialambre",
                    //    Key = objectKey
                    //};

                    //await s3Client.DeleteObjectAsync(deleteRequest);

                    //Codigo para eliminar el archivo local
                    string filePath = Path.Combine(webroot, brief.RespuestaCostosInput!);
                    FileManager.DeletefileCostos(filePath);

                    // Cambia el campo AsigandoCostos a "Asignado"
                    brief.ValorTotalCotizado = brief.ValorTotalCotizado;
                    brief.RespuestaCostos = null;
                    brief.RespuestaCostosInput = null;
                    brief.FechaRespuestaCostos = DateTime.Now;

                    // Guarda los cambios en la base de datos
                    await _briefService.UpdateBriefAsync(brief);

                    // Redirige de vuelta a la página de briefedit
                    return RedirectToPage("/Briefs/Employee/Costos/Aprobar", new { briefid = brief.BriefId });
                }
                else
                {
                    return RedirectToPage("/Briefs/Employee/Costos/Aprobar", new { briefid });
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }
    }
}

