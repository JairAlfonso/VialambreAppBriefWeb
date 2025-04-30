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
    public class ReenviarModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BriefService _briefService;
        private readonly IWebHostEnvironment _host; //Descomentar para guardar archivos en local  
        private readonly IToastNotification _notify;

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        public IEnumerable<SelectListItem>? GetProducts { get; set; }

        public IEnumerable<SelectListItem>? GetEmpaques { get; set; }

        public IEnumerable<SelectListItem>? GetLugarEntregas { get; set; }

        public IEnumerable<SelectListItem>? GetClientes { get; set; }

        public ReenviarModel(UserManager<AppUser> userManager, BriefService briefService, IWebHostEnvironment host, IToastNotification notify)
        {
            _userManager = userManager;
            _briefService = briefService;
            _host = host; 
            _notify = notify;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Asesor"))
            {
                return RedirectToPage("/Users/AccessDenied");
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

            // Usa BriefService para obtener la lista de clientes
            var clients = await _briefService.GetClientsAsync();

            // Convierte la lista de clientes en una lista de SelectListItem
            GetClientes = clients.Select(p => new SelectListItem
            {
                Value = p.Document, // Establece el valor en el nombre del cliente
                Text = p.Name // Establece el texto en el nombre del cliente                    
            }).ToList();

            BriefDetails = await _briefService.GetBriefAsync(id);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                return NotFound();
            }

            // Establece el valor inicial de los campos en BriefVM
            BriefVM = new BriefVM
            {
                Consecutivo = BriefDetails.Consecutivo,

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
                NewProducto = BriefDetails.NewProducto,
                FechaRequerida = BriefDetails!.FechaRequerida,
                EntregarEn = BriefDetails!.EntregarEn,
                NewLugarEntrega = BriefDetails.NewLugarEntrega,
                EntregarEnInput = BriefDetails.EntregarEnInput,
                ComentariosAdicionales = BriefDetails!.ComentariosAdicionales,

                Design = BriefDetails.Design,
                Linea = BriefDetails!.Linea,
                EsCantidadUnica = BriefDetails.EsCantidadUnica,
                UnidadesInput = BriefDetails!.UnidadesInput,
                EsEscala = BriefDetails.EsEscala,
                EscalaInput = BriefDetails!.EscalaInput,
                Instalacion = BriefDetails.Instalacion,
                DireccionInstalacion = BriefDetails!.DireccionInstalacion,

                EsCanalVenta = BriefDetails.EsCanalVenta,
                CanalVentaInput = BriefDetails.CanalVentaInput,
                CanalVenta = BriefDetails!.CanalVenta,
                TipoEmpaque = BriefDetails!.TipoEmpaque,
                NewEmpaque = BriefDetails.NewEmpaque,
                TipoEmpaqueInput = BriefDetails.TipoEmpaqueInput,
                PresupuestoUnidad = BriefDetails.PresupuestoUnidad,

                Costos = BriefDetails.Costos,
                ArchivosCostosExist = BriefDetails.ArchivosCostosExist,
                ArchivosCostos = BriefDetails!.ArchivosCostos,
                ArchivosDesignExist = BriefDetails.ArchivosDesignExist,
                ArchivosDesign = BriefDetails!.ArchivosDesign,
                ImageUrlExist = BriefDetails.ImageUrlExist,
                ImageUrl = BriefDetails!.ImageUrl,

                KeyVisual = BriefDetails.KeyVisual,
                Artes = BriefDetails.Artes,
                Planos = BriefDetails.Planos,
                Logos = BriefDetails.Logos,
                ManualMarca = BriefDetails.ManualMarca,
                Boceto = BriefDetails.Boceto,
                MuestraFisica = BriefDetails.MuestraFisica,
                ImagenReferencia = BriefDetails.ImagenReferencia,
                Ninguna = BriefDetails.Ninguna,

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

            if (BriefVM.NewProducto == true && BriefVM.ProductoInput != null)
            {
                BriefVM.Producto = BriefVM.ProductoInput;
                BriefVM.NewProducto = false;
                BriefVM.ProductoInput = null;
            }
            else
            {
                BriefVM.NewProducto = false;
                BriefVM.ProductoInput = null;
            };

            if (BriefVM.NewLugarEntrega == true && BriefVM.EntregarEnInput != null)
            {
                BriefVM.EntregarEn = BriefVM.EntregarEnInput;
                BriefVM.NewLugarEntrega = false;
                BriefVM.EntregarEnInput = null;
            }
            else
            {
                BriefVM.NewLugarEntrega = false;
                BriefVM.EntregarEnInput = null;
            };

            if (BriefVM.AgregarNuevoCliente == true && BriefVM.NewClientFullName != null)
            {
                BriefVM.ClientFullName = BriefVM.NewClientFullName;
                BriefVM.ClientDocument = BriefVM.NewClientDocument;
                BriefVM.AgregarNuevoCliente = false;
                BriefVM.NewClientFullName = null;
                BriefVM.NewClientDocument = null;

            }
            else
            {
                BriefVM.AgregarNuevoCliente = false;
                BriefVM.NewClientDocument = null;
            }

            // Verifica si el campo es canal venta es true               
            if (BriefVM.EsCanalVenta == true && BriefVM!.CanalVentaInput != null)
            {
                BriefVM.CanalVenta = BriefVM!.CanalVentaInput;
                BriefVM.EsCanalVenta = true;
            }
            else
            {
                BriefVM.EsCanalVenta = false;
                BriefVM.CanalVentaInput = null;
            }

            // Agrega un nuevo empaque
            if (BriefVM.NewEmpaque == true && BriefVM.TipoEmpaqueInput != null)
            {
                BriefVM.TipoEmpaque = BriefVM!.TipoEmpaqueInput;
                BriefVM.NewEmpaque = false;
                BriefVM.TipoEmpaqueInput = null;
            }
            else
            {
                BriefVM.NewEmpaque = false;
                BriefVM.TipoEmpaqueInput = null;
            }

            return Page();
        }


        public async Task<IActionResult> OnGetEliminarArchivoCostos(int id)
        {
            //Activar para guardar archivos en local
            string webroot = _host.WebRootPath;

            if (User.IsInRole("Asesor"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(id);

                // Verifica si se encontró el brief
                if (brief != null && !string.IsNullOrEmpty(brief.ArchivosCostos))
                {
                    //// Código para eliminar la imagen antigua en bucket aws
                    //var credentials = new BasicAWSCredentials("AKIAVQ624AHFXLQI2TUU", "N4oIHlCI/tO753kpg+bXi0+xYWPEREJhVuKxQ4V3");
                    //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                    //// Obtiene el nombre del objeto de la URL
                    //var objectKey = brief.ArchivosCostos.Split('/').Last();

                    //var deleteRequest = new DeleteObjectRequest
                    //{
                    //    BucketName = "imagenesvialambre",
                    //    Key = objectKey
                    //};

                    //await s3Client.DeleteObjectAsync(deleteRequest);

                    //Codigo para eliminar el archivo local
                    string filePath = Path.Combine(webroot, brief.ArchivosCostos);
                    FileManager.DeletefileCostos(filePath);

                    // Cambia el campo AsigandoCostos a "Asignado"
                    brief.ArchivosCostosExist = false;
                    brief.ArchivosCostos = null;

                    // Guarda los cambios en la base de datos
                    await _briefService.UpdateBriefAsync(brief);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddWarningToastMessage("Archivos para costos eliminados!!");
                    return RedirectToPage("/Briefs/Client/Reenviar", new { id = brief.BriefId });
                }
                else
                {
                    return RedirectToPage("/Briefs/Client/Reenviar");
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }

        public async Task<IActionResult> OnGetEliminarArchivoDesign(int id)
        {
            //Activar para guardar archivos en local
            string webroot = _host.WebRootPath;

            if (User.IsInRole("Asesor"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(id);

                // Verifica si se encontró el brief
                if (brief != null && !string.IsNullOrEmpty(brief.ArchivosDesign))
                {
                    //// Código para eliminar la imagen antigua del bucket aws
                    //var credentials = new BasicAWSCredentials("AKIAVQ624AHFXLQI2TUU", "N4oIHlCI/tO753kpg+bXi0+xYWPEREJhVuKxQ4V3");
                    //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

                    //// Obtiene el nombre del objeto de la URL
                    //var objectKey = brief.ArchivosDesign.Split('/').Last();

                    //var deleteRequest = new DeleteObjectRequest
                    //{
                    //    BucketName = "imagenesvialambre",
                    //    Key = objectKey
                    //};

                    //await s3Client.DeleteObjectAsync(deleteRequest);

                    //Codigo para eliminar el archivo local
                    string filePath = Path.Combine(webroot, brief.ArchivosDesign);
                    FileManager.DeletefileCostos(filePath);

                    // Cambia el campo AsigandoCostos a "Asignado"
                    brief.ArchivosDesignExist = false;
                    brief.ArchivosDesign = null;

                    // Guarda los cambios en la base de datos
                    await _briefService.UpdateBriefAsync(brief);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddWarningToastMessage("Archivos para diseño eliminados!!");
                    return RedirectToPage("/Briefs/Client/Reenviar", new { id = brief.BriefId });
                }
                else
                {
                    return RedirectToPage("/Briefs/Client/Reenviar");
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }

        public async Task<IActionResult> OnGetEliminarArchivoOtros(int id)
        {
            //Activar para guardar archivos en local
            string webroot = _host.WebRootPath;

            if (User.IsInRole("Asesor"))
            {
                // Obtén el brief por su ID
                var brief = await _briefService.GetBriefDetailsAsync(id);

                // Verifica si se encontró el brief
                if (brief != null && !string.IsNullOrEmpty(brief.ImageUrl))
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
                    string filePath = Path.Combine(webroot, brief.ImageUrl);
                    FileManager.DeletefileCostos(filePath);

                    // Cambia el campo AsigandoCostos a "Asignado"
                    brief.ImageUrlExist = false;
                    brief.ImageUrl = null;

                    // Guarda los cambios en la base de datos
                    await _briefService.UpdateBriefAsync(brief);

                    // Redirige de vuelta a la página de briefedit
                    _notify.AddWarningToastMessage("Otros archivos eliminados!!");
                    return RedirectToPage("/Briefs/Client/Reenviar", new { id = brief.BriefId });
                }
                else
                {
                    return RedirectToPage("/Briefs/Client/Pendientes");
                }
            }
            else
            {
                return RedirectToPage("/Users/AccessDenied");
            }
        }


        public async Task<IActionResult> OnPostAsync(int id)
        {
            ////Codigo para guardar archivo en bucket aws
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
            var files = BriefDetails!.FileOtros; //HttpContext.Request.Form.Files["imageurlInput"];
            var filesCostos = BriefDetails!.FileCostos;//HttpContext.Request.Form.Files["filescostosInput"];
            var filesDesign = BriefDetails!.FileDesign; //HttpContext.Request.Form.Files["filesdesignInput"];
            string ImageFolder = @"Images\ArchivosOtros";
            string CostosFolder = @"Images\ArchivosCostos";
            string DesignFolder = @"Images\ArchivosDesign";
            string UploadFolder = Path.Combine(webroot, ImageFolder);
            string UploadFolderCostos = Path.Combine(webroot, CostosFolder);
            string UploadFolderDesign = Path.Combine(webroot, DesignFolder);

            var user = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Asesor"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            var brief = await _briefService.GetBriefAsync(id);

            if (brief == null)
            {
                // Manejar el caso en que el brief no se encuentre
                _notify.AddErrorToastMessage("No se encontró el brief");
                return NotFound();
            }

            brief.Consecutivo = brief.Consecutivo;
            brief.BriefId = brief.BriefId;

            brief.AsesorFullName = user.FullName;
            brief.AsesorDocument = user.Document;
            brief.AsesorEmail = user.Email;
            brief.AsesorPhoneNumber = user.PhoneNumber;

            brief.AgregarNuevoCliente = Request.Form.ContainsKey("AgregarNuevoCliente");
            brief.NewClientFullName = BriefDetails!.NewClientFullName;
            brief.NewClientDocument = BriefDetails!.NewClientDocument;
            brief.ClientFullName = BriefDetails!.ClientFullName;
            brief.ClientDocument = BriefDetails!.ClientDocument;

            brief.TipoCotizacion = BriefDetails.TipoCotizacion;
            brief.Title = BriefDetails!.Title;
            brief.Marca = BriefDetails!.Marca;
            brief.Description = BriefDetails!.Description;
            brief.Producto = BriefDetails!.Producto;
            brief.NewProducto = Request.Form.ContainsKey("NewProduct");
            brief.FechaRequerida = BriefDetails!.FechaRequerida;
            brief.EntregarEn = BriefDetails!.EntregarEn;
            brief.NewLugarEntrega = Request.Form.ContainsKey("NewLugarEntrega");
            brief.ComentariosAdicionales = BriefDetails!.ComentariosAdicionales;

           
            brief.Design = BriefDetails.Design;
            brief.Linea = BriefDetails!.Linea;
            brief.EsCantidadUnica = Request.Form.ContainsKey("EsCantidadUnica");
            brief.UnidadesInput = BriefDetails!.UnidadesInput;
            brief.EsEscala = Request.Form.ContainsKey("EsEscala");
            brief.EscalaInput = BriefDetails!.EscalaInput;
            brief.Instalacion = BriefDetails!.Instalacion;
            brief.DireccionInstalacion = BriefDetails!.DireccionInstalacion;

            brief.EsCanalVenta = Request.Form.ContainsKey("EsCanalVenta");
            brief.CanalVenta = BriefDetails.CanalVenta;
            brief.CanalVentaInput = BriefDetails.CanalVentaInput;
            brief.TipoEmpaque = BriefDetails!.TipoEmpaque;
            brief.NewEmpaque = Request.Form.ContainsKey("NewEmpaque");
            brief.PresupuestoUnidad = BriefDetails.PresupuestoUnidad;

            brief.Costos = BriefDetails.Costos;
            brief.ArchivosCostosExist = Request.Form.ContainsKey("ArchivosCostosExist");
            brief.FileCostos = BriefDetails!.FileCostos;
            brief.ArchivosCostos = BriefDetails!.ArchivosCostos;
            brief.ArchivosDesignExist = Request.Form.ContainsKey("ArchivosDesignExist");
            brief.FileDesign = BriefDetails!.FileDesign;
            brief.ArchivosDesign = BriefDetails!.ArchivosDesign;
            brief.ImageUrlExist = Request.Form.ContainsKey("ImageUrlExist");
            brief.FileOtros = BriefDetails!.FileOtros;
            brief.ImageUrl = BriefDetails!.ImageUrl;

            brief.KeyVisual = Request.Form.ContainsKey("KeyVisual");
            brief.Artes = Request.Form.ContainsKey("Artes");
            brief.Planos = Request.Form.ContainsKey("Planos");
            brief.Logos = Request.Form.ContainsKey("Logos");
            brief.ManualMarca = Request.Form.ContainsKey("ManualMarca");
            brief.Boceto = Request.Form.ContainsKey("Boceto");
            brief.MuestraFisica = Request.Form.ContainsKey("MuestraFisica");
            brief.ImagenReferencia = Request.Form.ContainsKey("ImagenReferencia");
            brief.Ninguna = Request.Form.ContainsKey("Ninguna");

            brief.CreateDate = brief.CreateDate;
            brief.UserId = brief.UserId;
           
            brief.RespuestaCostos = brief.RespuestaCostos;
            brief.FechaRespuestaCostos = brief.FechaRespuestaCostos;
            brief.EsRecotizacion = brief.EsRecotizacion;
            brief.FueRecotizado = brief.FueRecotizado;
            brief.AsignadoDesign = brief.AsignadoDesign;
            brief.DesignerAsignado = brief.DesignerAsignado;
            brief.AsignadoCostos = brief.AsignadoCostos;
            brief.CosteadorAsignado = brief.CosteadorAsignado;


            // Verifica si el campo diseño es falso 
            if (brief.StatusDesign == BriefStatus.Aceptado)
            {
                brief.Design = true;
            }
            else if (brief.StatusDesign == BriefStatus.SinDiseño)
            {
                brief.Design = false;
            }
            else if (brief.StatusDesign == BriefStatus.Rechazado)
            {
                brief.Design = true;
                brief.StatusDesign = BriefStatus.Pendiente;
            }
            else
            {
                brief.Design = true;
            }

            // Verifica si el campo costos es falso
            
            if (brief.StatusCostos == BriefStatus.Aceptado)
            {
                brief.Costos = true;
            }
            else if (brief.StatusCostos == BriefStatus.SinCostos)
            {
                brief.Costos = false;
            }
            else if (brief.StatusCostos == BriefStatus.Rechazado)
            {
                brief.Costos = true;
                brief.StatusCostos = BriefStatus.Pendiente;
            }
            else
            {
                brief.Costos = true;
            }

            ////Codigo para guardar archivos en bucket aws
            //// Verifica si se han cargado archivos para cotos
            //if (brief.ArchivosCostosExist == true && brief.FileCostos != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(brief.FileCostos!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = brief.FileCostos.OpenReadStream(),
            //        ContentType = brief.FileCostos.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    brief.ArchivosCostos = enlaceArchivo;
            //}
            //else if (brief.ArchivosCostosExist == true && brief.ArchivosCostos == null)
            //{
            //    brief.ArchivosCostosExist = false;
            //    brief.ArchivosCostos = "Sin archivos";
            //}

            //// Verifica si se han cargado archivos para diseño
            //if (brief.ArchivosDesignExist == true && brief.FileDesign != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(brief.FileDesign!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = brief.FileDesign.OpenReadStream(),
            //        ContentType = brief.FileDesign.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    brief.ArchivosDesign = enlaceArchivo;
            //}
            //else if (brief.ArchivosDesignExist == true && brief.ArchivosDesign == null)
            //{
            //    brief.ArchivosDesignExist = false;
            //    brief.ArchivosDesign = "Sin archivos";
            //}

            //// Verifica si se han cargado otros archivos 
            //if (brief.ImageUrlExist == true && brief.FileOtros != null)
            //{
            //    // Obtiene información sobre el archivo
            //    var fileExtension = Path.GetExtension(brief.FileOtros!.FileName);
            //    var key = $"{Guid.NewGuid()}{fileExtension}";

            //    // Configura la solicitud para subir el archivo
            //    var fileUploadRequest = new PutObjectRequest
            //    {
            //        BucketName = awsBucketName,
            //        Key = key,
            //        InputStream = brief.FileOtros.OpenReadStream(),
            //        ContentType = brief.FileOtros.ContentType
            //    };

            //    // Sube el archivo al bucket de AWS
            //    await s3Client.PutObjectAsync(fileUploadRequest);

            //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
            //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
            //    brief.ImageUrl = enlaceArchivo;
            //}
            //else if (brief.ImageUrlExist == true && brief.ImageUrl == null)
            //{
            //    brief.ImageUrlExist = false;
            //    brief.ImageUrl = "Sin archivos";
            //}


            //Codigo para guardar archivos en local
            // Verifica si se han cargado archivos para costos 
            if (brief.ArchivosCostosExist == true && brief.FileCostos != null )
            {
                string fileNewName = await FileManager.CopyFileCostos(filesCostos!, UploadFolderCostos);
                brief.ArchivosCostos = Path.Combine(CostosFolder, fileNewName);
            }
            else if(brief.ArchivosCostosExist == true && brief.ArchivosCostos == null)
            {
                brief.ArchivosCostosExist = false;
                brief.ArchivosCostos = null;
            }

            // Verifica si se han cargado archivos para diseño 
            if (brief.ArchivosDesignExist == true && brief.FileDesign != null)
            {
                string fileNewName = await FileManager.CopyFileDesign(filesDesign!, UploadFolderDesign);
                brief.ArchivosDesign = Path.Combine(DesignFolder, fileNewName);
            }
            else if(brief.ArchivosDesignExist == true && brief.ArchivosDesign == null)
            {
                brief.ArchivosDesignExist = false;
                brief.ArchivosDesign = null;
            }

            // Verifica si se han cargado otros archivos 
            if (brief.ImageUrlExist == true && brief.FileOtros != null)
            {
                string fileNewName = await FileManager.CopyFile(files!, UploadFolder);
                brief.ImageUrl = Path.Combine(ImageFolder, fileNewName);
            }
            else if(brief.ImageUrlExist == true && brief.ImageUrl == null)
            {
                brief.ImageUrlExist = false;
                brief.ImageUrl = null;
            }

            // Verifica si es Unidades o escala 
            if (brief.EsCantidadUnica == true)
            {
                brief.UnidadesInput = BriefDetails!.UnidadesInput;
                brief.EsEscala = false;
                brief.EscalaInput = "Sin escala";
            }
            else if (brief.EsEscala == true)
            {
                brief.EscalaInput = BriefDetails!.EscalaInput;
                brief.EsCantidadUnica = false;
                brief.UnidadesInput = "Sin unidades";
            }

            // Verifica si el campo instalacion es falso
            if (brief.Instalacion == true)
            {
                brief.DireccionInstalacion = BriefDetails!.DireccionInstalacion;
            }
            else
            {
                brief.DireccionInstalacion = "Sin instalacion";
            }

            // Verifica si el campo es canal venta es true               
            if (brief.EsCanalVenta == true)
            {
                brief.CanalVenta = BriefDetails!.CanalVentaInput;
            }
            else
            {
                brief.CanalVentaInput = brief.CanalVenta;
            }

            // Agrega un nuevo producto
            if (brief.NewProducto == true)
            {
                var newProduct = new Product
                {
                    Name = BriefDetails!.ProductoInput,
                };

                await _briefService.AddNewProductAsync(newProduct);
                brief.Producto = BriefDetails!.ProductoInput;
            }

            // Agrega un nuevo empaque
            if (brief.NewEmpaque == true)
            {
                var newEmpaque = new Empaque
                {
                    Name = BriefDetails!.TipoEmpaqueInput,
                };

                await _briefService.AddNewEmpaqueAsync(newEmpaque);
                brief.TipoEmpaque = BriefDetails!.TipoEmpaqueInput;
            }

            // Agrega un nuevo lugar de entrega
            if (brief.NewLugarEntrega == true)
            {
                var newLugarentrega = new LugarEntrega
                {
                    Name = BriefDetails!.EntregarEnInput,
                };

                await _briefService.AddNewLugarEntregaAsync(newLugarentrega);
                brief.EntregarEn = BriefDetails!.EntregarEnInput;
            }

            // Agrega un nuevo cliente
            var agregarNuevoCliente = Request.Form["AgregarNuevoCliente"];
            if (!string.IsNullOrEmpty(agregarNuevoCliente) && agregarNuevoCliente == "on")
            {
                var newClient = new Cliente
                {
                    Name = BriefVM!.NewClientFullName,
                    Document = BriefVM.NewClientDocument
                };

                await _briefService.AddNewClientAsync(newClient);
                brief.ClientFullName = BriefVM!.NewClientFullName;
                brief.ClientDocument = BriefVM!.NewClientDocument;
            }

            await _briefService.UpdateBriefAsync(brief);
            _notify.AddSuccessToastMessage("La cotización se actualizó!!");
            return RedirectToPage("/Briefs/Client/BriefReenviadoReadOnly", new { briefid = brief.BriefId });

        }
    }
}
