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
    public class CreateModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;
        private readonly BriefService _briefService;
        private readonly IWebHostEnvironment _host;

        [BindProperty]
        public BriefVM? BriefVM { get; set; }

        [BindProperty]
        public Pieza? Pieza { get; set; }

        [BindProperty]
        public Cliente? Cliente { get; set; }

        public IEnumerable<SelectListItem>? GetProducts { get; set; }

        public IEnumerable<SelectListItem>? GetEmpaques { get; set; }

        public IEnumerable<SelectListItem>? GetLugarEntregas { get; set; }

        public IEnumerable<SelectListItem>? GetClientes { get; set; }


        public CreateModel(UserManager<AppUser> userManager, IToastNotification notify, BriefService briefService, IWebHostEnvironment host)
        {
            _userManager = userManager;
            _notify = notify;
            _briefService = briefService;
            _host = host; //Descomentar para guardar archivos en local
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Inicializa BriefVM con los datos del usuario
                BriefVM = new BriefVM
                {
                    AsesorFullName = user.FullName,
                    AsesorDocument = user.Document,
                    AsesorEmail = user.Email,
                    AsesorPhoneNumber = user.PhoneNumber
                };

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

            }
            else
            {
                _notify.AddErrorToastMessage("El usuario NO esta autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //// Define tu información de acceso a AWS aquí //Codigo para guardar en bucket aws
            //var awsAccessKeyId = "AKIAVQ624AHFXLQI2TUU";
            //var awsSecretAccessKey = "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";
            //var awsBucketName = "imagenesvialambre";
            //// Configura las credenciales de AWS
            //var credentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            //// Configura el cliente de S3 de AWS
            //var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

            //Este codigo guarda los archivos en carpeta local
            string webroot = _host.WebRootPath;
            var files = BriefVM!.FileOtros; //HttpContext.Request.Form.Files["otrosarchivosInput"];
            var filesCostos = BriefVM!.FileCostos; //HttpContext.Request.Form.Files["archivoscostosInput"];
            var filesDesign = BriefVM!.FileDesign; //HttpContext.Request.Form.Files["archivosdesignInput"];
            string ImageFolder = @"Images\ArchivosOtros";
            string CostosFolder = @"Images\ArchivosCostos";
            string DesignFolder = @"Images\ArchivosDesign";
            string UploadFolder = Path.Combine(webroot, ImageFolder);
            string UploadFolderCostos = Path.Combine(webroot, CostosFolder);
            string UploadFolderDesign = Path.Combine(webroot, DesignFolder);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notify.AddErrorToastMessage("El usuario NO esta autenticado!!");
                return RedirectToPage("/Users/Login");
            }

            if (BriefVM != null)
            {
                // Generar el nuevo consecutivo
                string nuevoConsecutivo = await _briefService.GenerateConsecutivoAsync();

                var brief = new Brief
                {
                    Consecutivo = nuevoConsecutivo,

                    AsesorFullName = user.FullName,
                    AsesorDocument = user.Document,
                    AsesorEmail = user.Email,
                    AsesorPhoneNumber = user.PhoneNumber,

                    AgregarNuevoCliente = Request.Form.ContainsKey("AgregarNuevoCliente"),
                    NewClientFullName = BriefVM.NewClientFullName,
                    NewClientDocument = BriefVM.NewClientDocument,
                    ClientFullName = BriefVM.ClientFullName,
                    ClientDocument = BriefVM.ClientDocument,

                    TipoCotizacion = BriefVM.TipoCotizacion,
                    HV = BriefVM?.HV,
                    Title = BriefVM!.Title,
                    Marca = BriefVM!.Marca,
                    Description = BriefVM!.Description,
                    Producto = BriefVM!.Producto,
                    NewProducto = Request.Form.ContainsKey("NewProduct"),
                    ProductoInput = BriefVM.ProductoInput,
                    FechaRequerida = BriefVM!.FechaRequerida,
                    EntregarEn = BriefVM!.EntregarEn,
                    NewLugarEntrega = Request.Form.ContainsKey("NewLugarEntrega"),
                    EntregarEnInput = BriefVM.EntregarEnInput,
                    ComentariosAdicionales = BriefVM!.ComentariosAdicionales,
                    Costos = BriefVM!.Costos,
                    Design = BriefVM!.Design,
                    Linea = BriefVM!.Linea,
                    EsCantidadUnica = Request.Form.ContainsKey("EsCantidadUnica"),
                    UnidadesInput = BriefVM!.UnidadesInput,
                    EsEscala = Request.Form.ContainsKey("EsEscala"),
                    EscalaInput = BriefVM!.EscalaInput,
                    Instalacion = BriefVM!.Instalacion,
                    DireccionInstalacion = BriefVM!.DireccionInstalacion,

                    EsCanalVenta = Request.Form.ContainsKey("EsCanalVenta"),
                    CanalVentaInput = BriefVM!.CanalVentaInput,
                    CanalVenta = BriefVM!.CanalVenta,
                    TipoEmpaque = BriefVM!.TipoEmpaque,
                    NewEmpaque = Request.Form.ContainsKey("NewEmpaque"),
                    TipoEmpaqueInput = BriefVM.TipoEmpaqueInput,
                    PresupuestoUnidad = BriefVM.PresupuestoUnidad,
                    ValorTotalCotizado = BriefVM?.ValorTotalCotizado ?? 0m,

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




                    EsRecotizacion = false,
                    FueRecotizado = false,
                    AsignadoDesign = false,
                    DesignerAsignado = "Sin asignar",
                    AsignadoCostos = false,
                    CosteadorAsignado = "Sin asignar"

                };

                ////Codigo para guardar archivos en bucket aws
                //// Verifica si se han cargado archivos para costos  
                //if (BriefVM.ArchivosCostosExist == true && BriefVM.FileCostos != null)
                //{
                //    // Obtiene información sobre el archivo
                //    var fileExtension = Path.GetExtension(BriefVM.FileCostos!.FileName);
                //    var key = $"{Guid.NewGuid()}{fileExtension}";

                //    // Configura la solicitud para subir el archivo
                //    var fileUploadRequest = new PutObjectRequest
                //    {
                //        BucketName = awsBucketName,
                //        Key = key,
                //        InputStream = BriefVM.FileCostos.OpenReadStream(),
                //        ContentType = BriefVM.FileCostos.ContentType
                //    };

                //    // Sube el archivo al bucket de AWS
                //    await s3Client.PutObjectAsync(fileUploadRequest);

                //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
                //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
                //    brief.ArchivosCostos = enlaceArchivo;
                //}
                //else
                //{
                //    brief.ArchivosCostosExist = false;
                //    brief.ArchivosCostos = "Sin archivos";
                //}

                //// Verifica si se han cargado archivos para diseño
                //if (BriefVM.ArchivosDesignExist == true && BriefVM.FileDesign != null)
                //{
                //    // Obtiene información sobre el archivo
                //    var fileExtension = Path.GetExtension(BriefVM.FileDesign!.FileName);
                //    var key = $"{Guid.NewGuid()}{fileExtension}";

                //    // Configura la solicitud para subir el archivo
                //    var fileUploadRequest = new PutObjectRequest
                //    {
                //        BucketName = awsBucketName,
                //        Key = key,
                //        InputStream = BriefVM.FileDesign.OpenReadStream(),
                //        ContentType = BriefVM.FileDesign.ContentType
                //    };

                //    // Sube el archivo al bucket de AWS
                //    await s3Client.PutObjectAsync(fileUploadRequest);

                //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
                //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
                //    brief.ArchivosDesign = enlaceArchivo;
                //}
                //else
                //{
                //    brief.ArchivosDesignExist = false;
                //    brief.ArchivosDesign = "Sin archivos";
                //}

                //// Verifica si se han cargado otros archivos 
                //if (BriefVM.ImageUrlExist == true && BriefVM.FileOtros != null)
                //{
                //    // Obtiene información sobre el archivo
                //    var fileExtension = Path.GetExtension(BriefVM.FileOtros!.FileName);
                //    var key = $"{Guid.NewGuid()}{fileExtension}";

                //    // Configura la solicitud para subir el archivo
                //    var fileUploadRequest = new PutObjectRequest
                //    {
                //        BucketName = awsBucketName,
                //        Key = key,
                //        InputStream = BriefVM.FileOtros.OpenReadStream(),
                //        ContentType = BriefVM.FileOtros.ContentType
                //    };

                //    // Sube el archivo al bucket de AWS
                //    await s3Client.PutObjectAsync(fileUploadRequest);

                //    // Ahora, puedes almacenar el enlace a este archivo en tu modelo o base de datos
                //    var enlaceArchivo = $"https://{awsBucketName}.s3.amazonaws.com/{fileUploadRequest.Key}";
                //    brief.ImageUrl = enlaceArchivo;
                //}
                //else
                //{
                //    brief.ImageUrlExist = false;
                //    brief.ImageUrl = "Sin archivos";
                //}

                //Este codigo guarda los archivos en carpeta local
                // Verifica si se han cargado archivos para costos 
                if (BriefVM.ArchivosCostosExist == true && BriefVM.FileCostos != null)
                {
                    string fileNewName = await FileManager.CopyFileCostos(filesCostos!, UploadFolderCostos);
                    brief.ArchivosCostos = Path.Combine(CostosFolder, fileNewName);
                }
                else if (BriefVM.ArchivosCostosExist == null && BriefVM.FileCostos != null) 
                {
                    brief.ArchivosCostosExist = true;
                }
                else if(BriefVM.ArchivosCostosExist == true && BriefVM.FileCostos == null)
                {
                    brief.ArchivosCostosExist = false;
                    brief.ArchivosCostos = null;
                }                

                // Verifica si se han cargado archivos para diseño 
                if (BriefVM.ArchivosDesignExist == true && BriefVM.FileDesign != null)
                {
                    string fileNewName = await FileManager.CopyFileDesign(filesDesign!, UploadFolderDesign);
                    brief.ArchivosDesign = Path.Combine(DesignFolder, fileNewName);
                }
                else if (BriefVM.ArchivosDesignExist == true && BriefVM.FileDesign == null)
                {
                    brief.ArchivosDesignExist = false;
                    brief.ArchivosDesign = null;
                }

                // Verifica si se han cargado otros archivos 
                if (BriefVM.ImageUrlExist == true && BriefVM.FileOtros != null)
                {
                    string fileNewName = await FileManager.CopyFile(files!, UploadFolder);
                    brief.ImageUrl = Path.Combine(ImageFolder, fileNewName);
                }
                else if (BriefVM.ImageUrlExist == true && BriefVM.FileOtros == null)
                {
                    brief.ImageUrlExist = false;
                    brief.ImageUrl = null;
                }

                // Verifica si el campo diseño es falso 
                if (brief.Design == false)
                {
                    brief.StatusDesign = BriefStatus.SinDiseño;
                    brief.DesignerAsignado = "No requiere";
                }
                else
                {
                    brief.StatusDesign = BriefStatus.Pendiente;
                }

                // Verifica si el campo costo es falso 
                if (brief.Costos == false)
                {
                    brief.StatusCostos= BriefStatus.SinCostos;
                    brief.CosteadorAsignado = "No requiere";
                }
                else
                {
                    brief.StatusCostos= BriefStatus.Pendiente;
                }

                // Verifica si es Unidades o escala 
                if (brief.EsCantidadUnica == true)
                {
                    brief.UnidadesInput = BriefVM!.UnidadesInput;
                    brief.EsEscala = false;
                    brief.EscalaInput = "Sin escala";
                }
                else if (brief.EsEscala == true)
                {
                    brief.EscalaInput = BriefVM!.EscalaInput;
                    brief.EsCantidadUnica = false;
                    brief.UnidadesInput = "Sin unidades";
                }

                // Verifica si el campo instalacion es falso
                if (brief.Instalacion == true)
                {
                    brief.DireccionInstalacion = BriefVM!.DireccionInstalacion;
                }
                else
                {
                    brief.DireccionInstalacion = "Sin instalacion";
                }

                // Verifica si el campo es canal venta es true               
                if (brief.EsCanalVenta == true)
                {
                    brief.CanalVenta = BriefVM!.CanalVentaInput;
                }
                else if (Request.Form.TryGetValue("canalVenta", out var selectedValue))
                {
                    brief.CanalVenta = selectedValue;
                    brief.CanalVentaInput = selectedValue;
                }

                // Agrega un nuevo producto
                if (brief.NewProducto == true)
                {
                    if (BriefVM!.ProductoInput == null)
                    {
                        BriefVM!.ProductoInput = "No ingreso producto";
                    }
                    else
                    {
                        var newProduct = new Product
                        {
                            Name = BriefVM!.ProductoInput,
                        };

                        await _briefService.AddNewProductAsync(newProduct);
                        brief.Producto = BriefVM!.ProductoInput;
                    }
                }

                // Agrega un nuevo empaque
                if (brief.NewEmpaque == true)
                {
                    var newEmpaque = new Empaque
                    {
                        Name = BriefVM!.TipoEmpaqueInput,
                    };

                    await _briefService.AddNewEmpaqueAsync(newEmpaque);
                    brief.TipoEmpaque = BriefVM.TipoEmpaqueInput;
                }

                // Agrega un nuevo lugar de entrega
                if (brief.NewLugarEntrega == true)
                {
                    var newLugarentrega = new LugarEntrega
                    {
                        Name = BriefVM!.EntregarEnInput,
                    };

                    await _briefService.AddNewLugarEntregaAsync(newLugarentrega);
                    brief.EntregarEn = BriefVM.EntregarEnInput;
                }

                // Agrega un nuevo cliente                
                if (brief.AgregarNuevoCliente == true && brief.NewClientFullName != null)
                {
                    var newClient = new Cliente
                    {
                        Name = brief!.NewClientFullName,
                        Document = brief.NewClientDocument
                    };

                    await _briefService.AddNewClientAsync(newClient);
                    brief.AgregarNuevoCliente = false;
                    brief.ClientFullName = BriefVM!.NewClientFullName;                    
                    brief.ClientDocument = BriefVM!.NewClientDocument;
                }
                else
                {
                    brief.AgregarNuevoCliente = false;
                    brief.NewClientFullName = null;
                    brief.NewClientDocument = null;
                }

                await _briefService.AddBriefAsync(brief);
                _notify.AddSuccessToastMessage("Consecutivo asignado!!");
                return RedirectToPage("/Briefs/Client/BriefReadOnly", new { briefid = brief.BriefId });
            }
            else
            {
                _notify.AddSuccessToastMessage("Consecutivo asignado!!");
            }

            return Page();
        }

    }
}
