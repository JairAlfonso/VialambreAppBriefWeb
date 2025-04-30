using System.ComponentModel.DataAnnotations;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.ViewModels
{
    public class BriefVM
    {
        [Display(Name = "BriefId")]
        public int BriefId { get; set; }

        [Display(Name = "Consecutivo")]
        public string? Consecutivo { get; set; }


        [Display(Name = "Documento cliente")]
        public string? ClientDocument { get; set; }


        [Required(ErrorMessage = "elegir campo")]
        [Display(Name = "Cliente")]
        public string? ClientFullName { get; set; }


        [Display(Name = "AgregarNuevoCliente?")]
        public bool AgregarNuevoCliente { get; set; }

        [Display(Name = "Nuevo documento cliente")]
        public string? NewClientDocument { get; set; }

        [Display(Name = "Nuevo nombre cliente")]
        public string? NewClientFullName { get; set; }


        [Display(Name = "Documento asesor")]
        public string? AsesorDocument { get; set; }

        [Display(Name = "Nombre asesor")]
        public string? AsesorFullName { get; set; }


        [Display(Name = "Correo asesor")]
        public string? AsesorEmail { get; set; }

        [Display(Name = "Teléfono asesor")]
        public string? AsesorPhoneNumber { get; set; }


        [Display(Name = "Tipo de Cotizacion:")]
        public string? TipoCotizacion { get; set; }



        [Required(ErrorMessage = "{0} es un campo obligatorio")]
        [Display(Name = "Hoja de Vida")]
        public string? HV { get; set; }



      




        [Required(ErrorMessage = "{0} es un campo obligatorio")]
        [Display(Name = "Campaña")]
        public string? Title { get; set; }

        [Display(Name = "Marca:")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "{0} es un campo obligatorio")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "Tipo de producto:")]
        public string? Producto { get; set; }

        [Display(Name = "Otro producto?")]
        public bool NewProducto { get; set; }

        [Display(Name = "Otro producto")]
        public string? ProductoInput { get; set; }

        [Display(Name = "FechaRequerida")]
        public DateTime FechaRequerida { get; set; }

        [Display(Name = "Entregar en:")]
        public string? EntregarEn { get; set; }

        [Display(Name = "nuevoLugarEntrega?")]
        public bool NewLugarEntrega { get; set; }

        [Display(Name = "Nuevo LugarEntrega")]
        public string? EntregarEnInput { get; set; }

        [Display(Name = "Comentarios adicionales:")]
        public string? ComentariosAdicionales { get; set; }



       
        [Display(Name = "Requiere costos?")]
        public bool? Costos { get; set; }



       
        [Display(Name = "Requiere diseño?")]
        public bool? Design { get; set; }




      
        [Display(Name = "Agregar pieza")]
        public bool? AgregarPieza { get; set; }


        [Display(Name = "Linea:")]
        public string? Linea { get; set; }

        [Display(Name = "La cantidad es unica?")]
        public bool EsCantidadUnica { get; set; }

        [Display(Name = "Unidades:")]
        public string? UnidadesInput { get; set; }

        [Display(Name = "La cantidad es escala?")]
        public bool EsEscala { get; set; }

        [Display(Name = "Escala:")]
        public string? EscalaInput { get; set; }

       
        [Display(Name = "Instalación:")]
        public bool? Instalacion { get; set; }

        [Display(Name = "Direccion de instación:")]
        public string? DireccionInstalacion { get; set; }

        [Display(Name = "Canal de venta:")]
        public string? CanalVenta { get; set; }

        [Display(Name = "Es canal venta?")]
        public bool EsCanalVenta { get; set; }

        [Display(Name = "Canal de venta ingresado:")]
        public string? CanalVentaInput { get; set; }

        [Display(Name = "Empaque:")]
        public string? TipoEmpaque { get; set; }

        [Display(Name = "NuevoEmpaque?")]
        public bool NewEmpaque { get; set; }

        [Display(Name = "Nuevo Empaque")]
        public string? TipoEmpaqueInput { get; set; }

        [Display(Name = "Presupuesto por unidad:")]
        public int? PresupuestoUnidad { get; set; }

        [Required(ErrorMessage = "El Valor Total Cotizado es obligatorio")]
        public decimal? ValorTotalCotizado { get; set; }


        [Display(Name = "Archivos para costos?")]
        public bool? ArchivosCostosExist { get; set; }

        public IFormFile? FileCostos { get; set; }

        
        [Display(Name = "Archivos para costos")]
        public string? ArchivosCostos { get; set; }

       
        [Display(Name = "Archivos para diseño?")]
        public bool? ArchivosDesignExist { get; set; }

        public IFormFile? FileDesign { get; set; }

       
        [Display(Name = "Archivos para Diseño:")]
        public string? ArchivosDesign { get; set; }

        
        [Display(Name = "Otros archivos?")]
        public bool? ImageUrlExist { get; set; }

        public IFormFile? FileOtros { get; set; }

       
        [Display(Name = "Otros archivos:")]
        public string? ImageUrl { get; set; }


        [Display(Name = "Info Suministrada:")]
        public string? InfoSuministrada { get; set; }

        [Display(Name = "Key visual")]
        public bool KeyVisual { get; set; }

        [Display(Name = "Artes")]
        public bool Artes { get; set; }

        [Display(Name = "Planos")]
        public bool Planos { get; set; }

        [Display(Name = "Logos")]
        public bool Logos { get; set; }

        [Display(Name = "Manual de marca")]
        public bool ManualMarca { get; set; }

        [Display(Name = "Boceto")]
        public bool Boceto { get; set; }

        [Display(Name = "Muestra física")]
        public bool MuestraFisica { get; set; }

        [Display(Name = "Imagen de referencia")]
        public bool ImagenReferencia { get; set; }

        [Display(Name = "Ninguna")]
        public bool Ninguna { get; set; }



















        [Display(Name = "EstadoDiseño")]
        public BriefStatus StatusDesign { get; set; }

        [Display(Name = "Asignado a diseño?")]
        public bool AsignadoDesign { get; set; }

        [Display(Name = "Diseñador")]
        public string? DesignerAsignado { get; set; }

        [Display(Name = "RespuestaDesign")]
        public string? RespuestaDesign { get; set; }

        [Display(Name = "RespuestaDesignInput")]
        public DateTime? FechaRespuestaDesign { get; set; } = null;

        [Display(Name = "RespuestaAsesorDesign")]
        public string? RespuestaAsesorDesign { get; set; }

        [Display(Name = "FechaRespuestaAsesorDesign")]
        public DateTime? FechaRespuestaAsesorDesign { get; set; } = null;

        [Display(Name = "FechaDesignAceptado")]
        public DateTime FechaDesignAceptado { get; set; }


        [Display(Name = "EstadoComercial")]
        public BriefStatus StatusAsesor { get; set; }


        [Display(Name = "EstadoCostos")]
        public BriefStatus StatusCostos { get; set; }

        [Display(Name = "Asignado a costos?")]
        public bool AsignadoCostos { get; set; }

        [Display(Name = "Costeador")]
        public string? CosteadorAsignado { get; set; }

        [Required(ErrorMessage = "{0} es un campo obligatorio")]
        [Display(Name = "RespuestaCostos")]
        public string? RespuestaCostos { get; set; }


        [Required(ErrorMessage = "{0} es un campo obligatorio")]
        public IFormFile? FileRespCostos { get; set; }

       
        [Display(Name = "RespuestaCostosInput")]
        public string? RespuestaCostosInput { get; set; }

        [Display(Name = "EntregadoCostos")]
        public DateTime FechaRespuestaCostos { get; set; }


        [Display(Name = "RespuestaAsesorCostos")]
        public string? RespuestaAsesorCostos { get; set; }

        [Display(Name = "FechaRespuestaAsesorCostos")]
        public DateTime FechaRespuestaAsesorCostos { get; set; }

        [Display(Name = "FechaCostosAceptado")]
        public DateTime FechaCostosAceptado { get; set; }







        [Display(Name = "FechaCreado")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Es recotización?")]
        public bool EsRecotizacion { get; set; }

        [Display(Name = "Fue recotizado?")]
        public bool FueRecotizado { get; set; }

        [Display(Name = "Fecha recotización")]
        public DateTime FechaRecotizacion { get; set; }

        [Display(Name = "Fecha reenvio costos")]
        public DateTime? FechaReenvioCostos { get; set; }

    }
}
