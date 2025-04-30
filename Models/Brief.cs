using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VialambreAppTest1.Utility;

namespace VialambreAppTest1.Models
{
    public class Brief
    {
        [Key]
        public int BriefId { get; set; }

        public string? Consecutivo { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string? ClientFullName { get; set; }

        public string? ClientDocument { get; set; }

        public bool AgregarNuevoCliente { get; set; }

        public string? NewClientFullName { get; set; }

        public string? NewClientDocument { get; set; }


        public string? AsesorDocument { get; set; }

        public string? AsesorFullName { get; set; }

        public string? AsesorEmail { get; set; }

        public string? AsesorPhoneNumber { get; set; }


        public string? TipoCotizacion { get; set; }


        public string? HV { get; set; }


        public string? Title { get; set; }

        public string? Marca { get; set; }

        public string? Description { get; set; }

        public string? Producto { get; set; }

        public bool NewProducto { get; set; }

        public string? ProductoInput { get; set; }

        public DateTime FechaRequerida { get; set; }

        public string? EntregarEn { get; set; }

        public bool NewLugarEntrega { get; set; }

        public string? EntregarEnInput { get; set; }

        public string? ComentariosAdicionales { get; set; }

        public bool? Costos { get; set; }

        public bool? Design { get; set; }

        public string? Linea { get; set; }

        public bool EsCantidadUnica { get; set; }

        public string? UnidadesInput { get; set; }

        public bool EsEscala { get; set; }

        public string? EscalaInput { get; set; }

        public bool? Instalacion { get; set; }

        public string? DireccionInstalacion { get; set; }

        public string? CanalVenta { get; set; }

        public bool EsCanalVenta { get; set; }

        public string? CanalVentaInput { get; set; }

        public string? TipoEmpaque { get; set; }

        public bool NewEmpaque { get; set; }

        public string? TipoEmpaqueInput { get; set; }

        public int? PresupuestoUnidad { get; set; }

        //[Required(ErrorMessage = "El valor total cotizado es obligatorio.")]
        //[Range(0, double.MaxValue, ErrorMessage = "El valor no puede ser negativo.")]
        //public decimal ValorTotalCotizado { get; set; } = 0;

        public decimal? ValorTotalCotizado { get; set; }

        public bool? ArchivosCostosExist { get; set; }

        [NotMapped]
        public IFormFile? FileCostos { get; set; }

        public string? ArchivosCostos { get; set; }

        public bool? ArchivosDesignExist { get; set; }

        [NotMapped]
        public IFormFile? FileDesign { get; set; }

        public string? ArchivosDesign { get; set; }

        public bool? ImageUrlExist { get; set; }

        [NotMapped]
        public IFormFile? FileOtros { get; set; }

        public string? ImageUrl { get; set; }


        public bool KeyVisual { get; set; }

        public bool Artes { get; set; }

        public bool Planos { get; set; }

        public bool Logos { get; set; }

        public bool ManualMarca { get; set; }

        public bool Boceto { get; set; }

        public bool MuestraFisica { get; set; }

        public bool ImagenReferencia { get; set; }

        public bool Ninguna { get; set; }


        public DateTime CreateDate { get; set; }

        public bool EsRecotizacion { get; set; }

        public bool FueRecotizado { get; set; }

        public DateTime FechaRecotizacion { get; set; }

        public BriefStatus StatusDesign { get; set; }

        public bool AsignadoDesign { get; set; }

        public string? DesignerAsignado { get; set; }


        public string? RespuestaDesign { get; set; }

        public DateTime? FechaRespuestaDesign { get; set; } = null;


        public string? RespuestaAsesorDesign { get; set; }

        public DateTime? FechaRespuestaAsesorDesign { get; set; } = null;

        public DateTime FechaDesignAceptado { get; set; }

        /// pendiente de codificacion 

        public DateTime? FechaAsignacion { get; set; }

        //editar

        public BriefStatus StatusAsesor { get; set; }





        public BriefStatus StatusCostos { get; set; }

        public bool AsignadoCostos { get; set; }

        public string? CosteadorAsignado { get; set; }


        public string? RespuestaCostos { get; set; }

        [NotMapped]
        public IFormFile? FileRespCostos { get; set; }

        public string? RespuestaCostosInput { get; set; }

        public DateTime? FechaRespuestaCostos { get; set; } = null;


        public string? RespuestaAsesorCostos { get; set; }

        public DateTime? FechaRespuestaAsesorCostos { get; set; } = null;

        public DateTime FechaCostosAceptado { get; set; }



        [ForeignKey("UserId")]
        public string? UserId { get; set; }

        public AppUser? AppUser { get; set; }

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        public Product? Product { get; set; }

        [ForeignKey("EmpaqueId")]
        public int? EmpaqueId { get; set; }

        public Empaque? Empaque { get; set; }

        [ForeignKey("LugarEntregaId")]
        public int? LugarEntregaId { get; set; }

        public LugarEntrega? LugarEntrega { get; set; }

        [ForeignKey("PiezaId")]
        public int? PiezaId { get; set; }

        public Pieza? Pieza { get; set; }

    }
}