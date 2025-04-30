using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;


namespace VialambreAppTest1.Pages.Briefs.Employee.Costos
{
    public class AprobarModel : PageModel
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IToastNotification _notify;
        private readonly IWebHostEnvironment _host;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        public AprobarModel(/*UserManager<AppUser> userManager,*/ BriefService briefService, IToastNotification notify, IWebHostEnvironment host)
        {
            //_userManager = userManager;
            _briefService = briefService;
            _notify = notify;
            _host = host;
        }

        public async Task<IActionResult> OnGet(int briefid)
        {
            if (User.IsInRole("Costos"))
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
            var files = HttpContext.Request.Form.Files["FileRespCostos"];

            if (files! != null)
            {
                var brief = await _briefService.GetBriefAsync(BriefDetails!.BriefId);

                if (brief == null)
                {
                    // Manejar el caso en que el brief no se encuentre
                    _notify.AddErrorToastMessage("El brief no existe...");
                    return NotFound();
                }

                if (!User.IsInRole("Costos"))
                {
                    return RedirectToPage("/Users/AccessDenied");
                }

                ////Codig paara guardar archivos en ebucket aws
                //// Define tu información de acceso a AWS aquí
                //var awsAccessKeyId = "AKIAVQ624AHFXLQI2TUU";
                //var awsSecretAccessKey = "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";
                //var awsBucketName = "imagenesvialambre";
                //// Configura las credenciales de AWS
                //var credentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
                //// Configura el cliente de S3 de AWS
                //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                //Este codigo guarda los archivos en carpeta local
                string webroot = _host.WebRootPath;
                var filesCostos = HttpContext.Request.Form.Files["FileRespCostos"];
                string FolderCostos = @"Images\ArchivosRespuestaCostos";
                string UploadFolder = Path.Combine(webroot, FolderCostos);

                brief.ValorTotalCotizado = BriefVM!.ValorTotalCotizado ?? 0;
                brief.RespuestaCostos = BriefVM!.RespuestaCostos;
                brief.StatusCostos = BriefStatus.Entregado;
                brief.FechaRespuestaCostos = DateTime.Now;

                // Código para eliminar la archivo anterior
                if (!string.IsNullOrEmpty(brief.RespuestaCostosInput))
                {
                    ////Codigo para eliminar onjeto del bucket aws
                    //// Obtiene el nombre del objeto de la URL
                    //var objectKey = brief.RespuestaCostosInput.Split('/').Last();

                    //var deleteRequest = new DeleteObjectRequest
                    //{
                    //    BucketName = "imagenesvialambre",
                    //    Key = objectKey
                    //};

                    //await s3Client.DeleteObjectAsync(deleteRequest);

                    //Codigo para eliminar el archivo local anterior
                    string filePath = Path.Combine(webroot, brief.RespuestaCostosInput);
                    FileManager.DeletefileCostos(filePath);
                    brief.RespuestaCostosInput = null;

                }

                //if (files != null)
                //{
                //    //Codigo para garadr en bucket aws
                //    // Obtiene información sobre el archivo
                //    var fileExtension = Path.GetExtension(files.FileName);
                //    var key = $"{Guid.NewGuid()}{fileExtension}";

                //    // Configura la solicitud para subir el archivo
                //    var fileUploadRequest = new PutObjectRequest
                //    {
                //        BucketName = awsBucketName,
                //        Key = key,
                //        InputStream = files.OpenReadStream(),
                //        ContentType = files.ContentType
                //    };

                //    // Sube el archivo al bucket de AWS
                //    await s3Client.PutObjectAsync(fileUploadRequest);

                //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
                //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
                //    brief.RespuestaCostosInput = enlaceArchivo;
                //}
                //else
                //{
                //    _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                //    return RedirectToPage("/Briefs/Employee/Costos/Aprobar", new { briefid = BriefDetails!.BriefId });
                //}

                //Codigo para guardar archivos en local

                // Verifica si se han cargado archivos  
                if (filesCostos != null)
                {
                    string fileNewName = await FileManager.CopyFileRespCostos(filesCostos!, UploadFolder);
                    brief.RespuestaCostosInput = Path.Combine(FolderCostos, fileNewName);
                }
                else
                {
                    brief.RespuestaCostosInput = "Sin archivos";
                }

                await _briefService.UpdateBriefAsync(brief);
                _notify.AddSuccessToastMessage("Cotización entregada!!");
                return RedirectToPage("/Briefs/Employee/Costos/Details", new { id = BriefDetails!.BriefId });
            }
            else
            {
                _notify.AddErrorToastMessage("La respuesta no puede estar vacía!!");
                return RedirectToPage("/Briefs/Employee/Costos/Aprobar", new { briefid = BriefDetails!.BriefId });
            }

        }
    }
}
