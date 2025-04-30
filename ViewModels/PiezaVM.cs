using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VialambreAppTest1.ViewModels
{
    public class PiezaVM
    {
        [Display(Name = "BriefId")]
        public int? BriefId { get; set; }

        [Display(Name = "Consecutivo")]
        public string? Consecutivo { get; set; }

        [Required(ErrorMessage = "Nombre de la pieza es un campo obligatorio")]
        [Display(Name = "Nombre de la pieza:")]
        public string? PiezaName { get; set; }

        [Display(Name = "Número de pieza:")]
        public int? NumeroPieza { get; set; }


        //editado


        [Required(ErrorMessage = "Cantidad de piezas es un campo obligatorio")]
        [Display(Name = "Cantidad de piezas:")]
        public string? CantidadPiezas { get; set; }


        [Required(ErrorMessage = "*Se requiere")]
        [Display(Name = "Pieza")]
        public bool? AgregarComoNueva { get; set; }



        [Required(ErrorMessage = "*Se requiere")]
        [Display(Name = "Alto:")]
        public string? PiezaAlto { get; set; }

        [Required(ErrorMessage = "*Se requiere")]
        [Display(Name = "Ancho:")]
        public string? PiezaAncho { get; set; }

        [Required(ErrorMessage = "*Se requiere")]
        [Display(Name = "Fondo:")]
        public string? PiezaFondo { get; set; }

        [Display(Name = "Material:")]
        public string? MaterialInput { get; set; }

        [Display(Name = "Tubo")]
        public bool Tubo { get; set; }

        [Display(Name = "Alambre")]
        public bool Alambre { get; set; }

        [Display(Name = "Lamina")]
        public bool Lamina { get; set; }

        [Display(Name = "Madera")]
        public bool Madera { get; set; }

        [Display(Name = "Formica")]
        public bool Formica { get; set; }

        [Display(Name = "Polietileno")]
        public bool Polietileno { get; set; }

        [Display(Name = "Acrílico")]
        public bool Acrilico { get; set; }

        [Display(Name = "PS")]
        public bool Ps { get; set; }

        [Display(Name = "PP")]
        public bool Pp { get; set; }

        [Display(Name = "MDF")]
        public bool Mdf { get; set; }

        [Display(Name = "Ninguno")]
        public bool NingunMaterial { get; set; }

        [Required(ErrorMessage = "*Se requiere")]
        [Display(Name = "Publicidad:")]
        public bool? Publicidad { get; set; }

        [Display(Name = "Color:")]
        public string? Color { get; set; }

        [Display(Name = "Tintas:")]
        public string? Tintas { get; set; }

        [Display(Name = "Acabado:")]
        public string? AcabadoInput { get; set; }

        [Display(Name = "Laminado:")]
        public bool Laminado { get; set; }

        [Display(Name = "Refilado")]
        public bool Refilado { get; set; }

        [Display(Name = "Troquelado")]
        public bool Troquelado { get; set; }

        [Display(Name = "Termodoblado")]
        public bool Termodoblado { get; set; }

        [Display(Name = "Termoformado")]
        public bool Termoformado { get; set; }

        [Display(Name = "Ninguno")]
        public bool NingunAcabado { get; set; }


        [Required(ErrorMessage = "El cargue es un dato obligatorio")]
        [Display(Name = "Cargue por pieza:")]
        public float? Cargue { get; set; }


        [Display(Name = "Acabados y accesorios:")]
        public string? AcabadosyAccesoriosInput { get; set; }

        [Display(Name = "Ruedas")]
        public bool Ruedas { get; set; }

        [Display(Name = "Niveladores")]
        public bool Niveladores { get; set; }

        [Display(Name = "Cromado")]
        public bool Cromado { get; set; }

        [Display(Name = "Zincado")]
        public bool Zincado { get; set; }

        [Display(Name = "Ninguno")]
        public bool NingunAccesorio { get; set; }

        [Display(Name = "Comentarios adicionales")]
        public string? ComentariosAdicionales { get; set; }




        [Display(Name = "Item1name")]
        public string? Item1name { get; set; }

        [Display(Name = "Item1Alto")]
        public string? Item1Alto { get; set; }

        [Display(Name = "Item1Ancho")]
        public string? Item1Ancho { get; set; }


        [Display(Name = "Item2name")]
        public string? Item2name { get; set; }

        [Display(Name = "Item2Alto")]
        public string? Item2Alto { get; set; }

        [Display(Name = "Item2Ancho")]
        public string? Item2Ancho { get; set; }


        [Display(Name = "Item3name")]
        public string? Item3name { get; set; }

        [Display(Name = "Item3Alto")]
        public string? Item3Alto { get; set; }

        [Display(Name = "Item3Ancho")]
        public string? Item3Ancho { get; set; }


        [Display(Name = "Item4name")]
        public string? Item4name { get; set; }

        [Display(Name = "Item4Alto")]
        public string? Item4Alto { get; set; }

        [Display(Name = "Item4Ancho")]
        public string? Item4Ancho { get; set; }


        [NotMapped]
        public IFormFile? FilePieza { get; set; }

        [Display(Name = "ArchivoPieza")]
        public string? ArchivoPieza { get; set; }



    }
}

