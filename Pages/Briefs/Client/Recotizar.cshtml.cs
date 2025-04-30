using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Client
{
    public class RecotizarModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IWebHostEnvironment _host; //Descomentar para guardar archivos en local
        private readonly IToastNotification _notify;

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public Brief? BriefOriginal { get; set; }

        [BindProperty]
        public BriefVM? BriefVM { get; set; }


        public IEnumerable<SelectListItem>? GetProducts { get; set; }

        public IEnumerable<SelectListItem>? GetEmpaques { get; set; }

        public IEnumerable<SelectListItem>? GetLugarEntregas { get; set; }

        public RecotizarModel(UserManager<AppUser> userManager, BriefService briefService, IWebHostEnvironment host, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _host = host; //Descomentar para guardar archivos en local
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Asesor"))
            {
                _notify.AddErrorToastMessage("El usuario NO esta autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            // Usa BriefService para obtener la lista de productos
            var products = await _briefService.GetProductsAsync();

            // Convierte la lista de productos en una lista de SelectListItem
            GetProducts = products.Select(p => new SelectListItem
            {
                Value = p.Name, // Establece el valor en el nombre del producto
                Text = p.Name // Establece el texto en el nombre del producto
            }).ToList();


            // Usa BriefService para obtener la lista de empaques
            var empaques = await _briefService.GetEmpaquesAsync();

            // Convierte la lista de empaques en una lista de SelectListItem
            GetEmpaques = empaques.Select(p => new SelectListItem
            {
                Value = p.Name, // Establece el valor en el nombre del empaque
                Text = p.Name // Establece el texto en el nombre del empaque
            }).ToList();


            // Usa BriefService para obtener la lista de lugares de entrega
            var lugarentrega = await _briefService.GetLugarEntregaAsync();

            // Convierte la lista de lugares en una lista de SelectListItem
            GetLugarEntregas = lugarentrega.Select(p => new SelectListItem
            {
                Value = p.Name, // Establece el valor en el nombre del empaque
                Text = p.Name // Establece el texto en el nombre del empaque
            }).ToList();

            BriefDetails = await _briefService.GetBriefForRecotzacionAsync(id);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                return NotFound();
            }

            // Establece el valor inicial de los campos en BriefVM
            BriefVM = new BriefVM
            {
                Consecutivo = BriefDetails.Consecutivo,
                BriefId = BriefDetails.BriefId,

                AsesorFullName = user.FullName,
                AsesorDocument = user.Document,
                AsesorEmail = user.Email,
                AsesorPhoneNumber = user.PhoneNumber,

                ClientFullName = BriefDetails.ClientFullName,
                ClientDocument = BriefDetails.ClientDocument,

                TipoCotizacion = BriefDetails.TipoCotizacion,
                Title = BriefDetails!.Title,
                Marca = BriefDetails!.Marca,
                Description = BriefDetails!.Description,
                Producto = BriefDetails!.Producto,
                NewProducto = BriefDetails!.NewProducto,
                FechaRequerida = BriefDetails!.FechaRequerida,
                EntregarEn = BriefDetails!.EntregarEn,
                NewLugarEntrega = BriefDetails!.NewLugarEntrega,
                ComentariosAdicionales = BriefDetails!.ComentariosAdicionales,

                Design = BriefDetails.Design,
                Linea = BriefDetails!.Linea,
                EsCantidadUnica = BriefDetails!.EsCantidadUnica,
                UnidadesInput = BriefDetails!.UnidadesInput,
                EsEscala = BriefDetails!.EsEscala,
                EscalaInput = BriefDetails!.EscalaInput,
                Instalacion = BriefDetails!.Instalacion,
                DireccionInstalacion = BriefDetails!.DireccionInstalacion,

                EsCanalVenta = BriefDetails!.EsCanalVenta,
                CanalVenta = BriefDetails!.CanalVenta,
                TipoEmpaque = BriefDetails!.TipoEmpaque,
                NewEmpaque = BriefDetails!.NewEmpaque,
                PresupuestoUnidad = BriefDetails.PresupuestoUnidad,

                KeyVisual = BriefDetails.KeyVisual,
                Artes = BriefDetails.Artes,
                Planos = BriefDetails.Planos,
                Logos = BriefDetails.Logos,
                ManualMarca = BriefDetails.ManualMarca,
                Boceto = BriefDetails.Boceto,
                MuestraFisica = BriefDetails.MuestraFisica,
                ImagenReferencia = BriefDetails.ImagenReferencia,
                Ninguna = BriefDetails.Ninguna,

                Costos = BriefDetails.Costos,
                CreateDate = BriefDetails.CreateDate,
                StatusDesign = BriefDetails.StatusDesign,
                StatusCostos = BriefDetails.StatusCostos,
                EsRecotizacion = BriefDetails.EsRecotizacion,
                FueRecotizado = BriefDetails.FueRecotizado,
                AsignadoDesign = BriefDetails.AsignadoDesign,
                DesignerAsignado = BriefDetails.DesignerAsignado,
                AsignadoCostos = BriefDetails.AsignadoCostos,
                CosteadorAsignado = BriefDetails.CosteadorAsignado,

            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Asesor"))
            {
                return RedirectToPage("/AccessDenied");
            }

            BriefOriginal = await _briefService.GetBriefForRecotzacionAsync(id);

            // Marcar el brief original como recotizado
            BriefOriginal!.FueRecotizado = true;
            BriefOriginal!.FechaRecotizacion = DateTime.Now;

            // Verificar si ya existe "_" en el consecutivo
            var consecutivo = BriefOriginal.Consecutivo;
            var version = 0;

            if (consecutivo!.Contains('_'))
            {
                var lastUnderscoreIndex = consecutivo.LastIndexOf("_");
                var versionString = consecutivo[(lastUnderscoreIndex + 1)..];

                if (int.TryParse(versionString, out int lastVersion))
                {
                    version = lastVersion;
                }

                // Remover el sufijo de versión del consecutivo original
                consecutivo = consecutivo[..lastUnderscoreIndex];
            }

            // Incrementamos la versión en 1
            version++;

            // Generar el nuevo consecutivo
            consecutivo = $"{consecutivo}_{version}";

            ////Codigo para guardar archivos en bucket aws
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
            var files = BriefVM!.FileOtros; //HttpContext.Request.Form.Files["imageurlInput"];
            var filesCostos = BriefVM!.FileCostos; //HttpContext.Request.Form.Files["filescostosInput"];
            var filesDesign = BriefVM!.FileDesign; //HttpContext.Request.Form.Files["filesdesignInput"];
            string ImageFolder = @"Images\ArchivosOtros";
            string CostosFolder = @"Images\ArchivosCostos";
            string DesignFolder = @"Images\ArchivosDesign";
            string UploadFolder = Path.Combine(webroot, ImageFolder);
            string UploadFolderCostos = Path.Combine(webroot, CostosFolder);
            string UploadFolderDesign = Path.Combine(webroot, DesignFolder);

            // Copiar valores de campos editables al briefRecotizado
            var briefRecotizado = new Brief
            {
                Consecutivo = consecutivo,

                AsesorFullName = user.FullName,
                AsesorDocument = user.Document,
                AsesorEmail = user.Email,
                AsesorPhoneNumber = user.PhoneNumber,

                ClientFullName = BriefDetails!.ClientFullName,
                ClientDocument = BriefDetails!.ClientDocument,

                TipoCotizacion = BriefDetails.TipoCotizacion,
                Title = BriefDetails!.Title,
                Marca = BriefDetails!.Marca,
                Description = BriefDetails!.Description,
                Producto = BriefDetails!.Producto,
                NewProducto = Request.Form.ContainsKey("NewProduct"),
                FechaRequerida = BriefDetails!.FechaRequerida,
                EntregarEn = BriefDetails!.EntregarEn,
                NewLugarEntrega = Request.Form.ContainsKey("NewLugarEntrega"),
                ComentariosAdicionales = BriefDetails!.ComentariosAdicionales,

                Design = BriefDetails.Design,
                Linea = BriefDetails!.Linea,
                EsCantidadUnica = Request.Form.ContainsKey("EsCantidadUnica"),
                UnidadesInput = BriefDetails!.UnidadesInput,
                EsEscala = Request.Form.ContainsKey("EsEscala"),
                EscalaInput = BriefDetails!.EscalaInput,
                Instalacion = BriefDetails!.Instalacion,
                DireccionInstalacion = BriefDetails!.DireccionInstalacion,

                EsCanalVenta = Request.Form.ContainsKey("EsCanalVenta"),
                CanalVentaInput = BriefDetails!.CanalVentaInput,
                CanalVenta = BriefDetails!.CanalVenta,
                TipoEmpaque = BriefDetails!.TipoEmpaque,
                NewEmpaque = Request.Form.ContainsKey("NewEmpaque"),
                PresupuestoUnidad = BriefDetails.PresupuestoUnidad,


                Costos = BriefDetails.Costos,
                ArchivosCostosExist = BriefVM!.ArchivosCostosExist,
                FileCostos = BriefVM!.FileCostos,
                ArchivosCostos = BriefVM!.ArchivosCostos,
                ArchivosDesignExist = BriefVM!.ArchivosDesignExist,
                FileDesign = BriefVM!.FileDesign,
                ArchivosDesign = BriefVM!.ArchivosDesign,
                ImageUrlExist = BriefVM!.ImageUrlExist,
                FileOtros = BriefVM!.FileOtros,
                ImageUrl = BriefVM!.ImageUrl,

                KeyVisual = Request.Form.ContainsKey("KeyVisual"),
                Artes = Request.Form.ContainsKey("Artes"),
                Planos = Request.Form.ContainsKey("Planos"),
                Logos = Request.Form.ContainsKey("Logos"),
                ManualMarca = Request.Form.ContainsKey("ManualMarca"),
                Boceto = Request.Form.ContainsKey("Boceto"),
                MuestraFisica = Request.Form.ContainsKey("MuestraFisica"),
                ImagenReferencia = Request.Form.ContainsKey("ImagenReferencia"),
                Ninguna = Request.Form.ContainsKey("Ninguna"),

                CreateDate = DateTime.Now,
                UserId = user.Id,
                StatusCostos = BriefStatus.Pendiente,
                EsRecotizacion = true,
                FueRecotizado = false,
                AsignadoDesign = false,
                DesignerAsignado = "Sin asignar",
                AsignadoCostos = false,
                CosteadorAsignado = "Sin asignar"

            };

            ////Codigo para guardar archivos en bucket aws
            //// Verifica si se han cargado archivos para costos  
            //if (briefRecotizado.ArchivosCostosExist == true && briefRecotizado.FileCostos != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(briefRecotizado.FileCostos!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = briefRecotizado.FileCostos.OpenReadStream(),
            //        ContentType = briefRecotizado.FileCostos.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    briefRecotizado.ArchivosCostos = enlaceArchivo;
            //}
            //else
            //{
            //    briefRecotizado.ArchivosCostosExist = false;
            //    briefRecotizado.ArchivosCostos = "Sin archivos";
            //}

            //// Verifica si se han cargado archivos para diseño
            //if (briefRecotizado.ArchivosDesignExist == true && briefRecotizado.FileDesign != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(briefRecotizado.FileDesign!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = briefRecotizado.FileDesign.OpenReadStream(),
            //        ContentType = briefRecotizado.FileDesign.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    briefRecotizado.ArchivosDesign = enlaceArchivo;
            //}
            //else
            //{
            //    briefRecotizado.ArchivosDesignExist = false;
            //    briefRecotizado.ArchivosDesign = "Sin archivos";
            //}

            //// Verifica si se han cargado otros archivos 
            //if (briefRecotizado.ImageUrlExist == true && briefRecotizado.FileOtros != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(briefRecotizado.FileOtros!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = briefRecotizado.FileOtros.OpenReadStream(),
            //        ContentType = briefRecotizado.FileOtros.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    briefRecotizado.ImageUrl = enlaceArchivo;
            //}
            //else
            //{
            //    briefRecotizado.ImageUrlExist = false;
            //    briefRecotizado.ImageUrl = "Sin archivos";
            //}

            //Codigo para guardar archivos en local
            // Verifica si se han cargado otros archivos 
            if (BriefVM.ImageUrlExist == true && BriefVM.FileOtros != null)
            {
                string fileNewName = await FileManager.CopyFile(files!, UploadFolder);
                briefRecotizado.ImageUrl = Path.Combine(ImageFolder, fileNewName);
            }
            else if (BriefVM.ImageUrlExist == true && BriefVM.ImageUrl == null)
            {
                briefRecotizado.ImageUrlExist = false;
                briefRecotizado.ImageUrl = null;
            }

            // Verifica si se han cargado archivos para costos 
            if (BriefVM.ArchivosCostosExist == true && BriefVM.FileCostos != null)
            {
                string fileNewName = await FileManager.CopyFileCostos(filesCostos!, UploadFolderCostos);
                briefRecotizado.ArchivosCostos = Path.Combine(CostosFolder, fileNewName);
            }
            else if (BriefVM.ArchivosCostosExist == true && BriefVM.ArchivosCostos == null)
            {
                briefRecotizado.ArchivosCostosExist = false;
                briefRecotizado.ArchivosCostos = null;
            }

            // Verifica si se han cargado archivos para diseño 
            if (BriefVM.ArchivosDesignExist == true && BriefVM.FileDesign != null)
            {
                string fileNewName = await FileManager.CopyFileDesign(filesDesign!, UploadFolderDesign);
                briefRecotizado.ArchivosDesign = Path.Combine(DesignFolder, fileNewName);
            }
            else if (BriefVM.ArchivosDesignExist == true && BriefVM.ArchivosDesign == null)
            {
                briefRecotizado.ArchivosDesignExist = false;
                briefRecotizado.ArchivosDesign = null;
            }

            // Verifica si el campo diseño es falso 
            if (BriefDetails.Design == false)
            {
                briefRecotizado.StatusDesign = BriefStatus.SinDiseño;
            }
            else
            {
                briefRecotizado.StatusDesign = BriefStatus.Pendiente;
            }
           
            if (BriefDetails.Costos == false)
            {
                briefRecotizado.StatusCostos = BriefStatus.SinCostos;
            }
            else
            {
                briefRecotizado.StatusCostos = BriefStatus.Pendiente;
            }

            // Verifica si es Unidades o escala 
            if (briefRecotizado.EsCantidadUnica == true)
            {
                briefRecotizado.UnidadesInput = BriefDetails!.UnidadesInput;
                briefRecotizado.EsEscala = false;
                briefRecotizado.EscalaInput = "Sin escala";
            }
            else if (briefRecotizado.EsEscala == true)
            {
                briefRecotizado.EscalaInput = BriefDetails!.EscalaInput;
                briefRecotizado.EsCantidadUnica = false;
                briefRecotizado.UnidadesInput = "Sin unidades";
            }

            // Verifica si el campo instalacion es falso
            if (BriefDetails.Instalacion == true)
            {
                briefRecotizado.DireccionInstalacion = BriefDetails!.DireccionInstalacion;
            }
            else
            {
                briefRecotizado.DireccionInstalacion = "Sin instalacion";
            }

            // Verifica si el campo es canal venta es true               
            if (BriefDetails.EsCanalVenta == true)
            {
                briefRecotizado.CanalVenta = BriefDetails!.CanalVentaInput;
            }
            else if (Request.Form.TryGetValue("canalVenta", out var selectedValue))
            {
                briefRecotizado.CanalVenta = selectedValue;
                briefRecotizado.CanalVentaInput = selectedValue;
            }

            // Agrega un nuevo producto
            if (BriefDetails.NewProducto == true)
            {
                var newProduct = new Product
                {
                    Name = BriefDetails!.ProductoInput,
                };

                await _briefService.AddNewProductAsync(newProduct);
                briefRecotizado.Producto = BriefDetails!.ProductoInput;
            }

            // Agrega un nuevo empaque
            if (BriefDetails.NewEmpaque == true)
            {
                var newEmpaque = new Empaque
                {
                    Name = BriefDetails!.TipoEmpaque,
                };

                await _briefService.AddNewEmpaqueAsync(newEmpaque);
                briefRecotizado.TipoEmpaque = BriefDetails!.TipoEmpaque;
            }

            // Agrega un nuevo lugar de entrega
            if (BriefDetails.NewLugarEntrega == true)
            {
                var newLugarentrega = new LugarEntrega
                {
                    Name = BriefDetails!.EntregarEnInput,
                };

                await _briefService.AddNewLugarEntregaAsync(newLugarentrega);
                briefRecotizado.EntregarEn = BriefDetails!.EntregarEnInput;
            }

            // Guardar ambos briefs en la base de datos
            await _briefService.UpdateBriefAsync(BriefOriginal);
            await _briefService.AddBriefAsync(briefRecotizado);
            _notify.AddSuccessToastMessage("Consecutivo asignado!!");
            return RedirectToPage("/Briefs/Client/BriefRecotizado", new { briefid = briefRecotizado.BriefId, id = BriefOriginal.BriefId, consecutivo = briefRecotizado.Consecutivo });
        }
    }
}
