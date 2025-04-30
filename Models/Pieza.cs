using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VialambreAppTest1.Models
{
    public class Pieza
    {
        [Key]
        public int Id { get; set; }

        public int? BriefId { get; set; }

        public string? Consecutivo { get; set; }

        public string? PiezaName { get; set; }

        public int? NumeroPieza { get; set; }

        public string? CantidadPiezas { get; set; }

        public string? PiezaAlto { get; set; }

        public string? PiezaAncho { get; set; }

        public string? PiezaFondo { get; set; }

        public string? MaterialInput { get; set; }

        public bool Tubo { get; set; }

        public bool Alambre { get; set; }

        public bool Lamina { get; set; }

        public bool Madera { get; set; }

        public bool Formica { get; set; }

        public bool Polietileno { get; set; }

        public bool Acrilico { get; set; }

        public bool Ps { get; set; }

        public bool Pp { get; set; }

        public bool Mdf { get; set; }

        public bool NingunMaterial { get; set; }

        public string? Color { get; set; }


        public bool? Publicidad { get; set; }


        public string? Tintas { get; set; }

        public string? AcabadoInput { get; set; }

        public bool Laminado { get; set; }

        public bool Refilado { get; set; }

        public bool Troquelado { get; set; }

        public bool Termodoblado { get; set; }

        public bool Termoformado { get; set; }

        public bool NingunAcabado { get; set; }

        public float? Cargue { get; set; }


        public string? AcabadosyAccesoriosInput { get; set; }

        public bool Ruedas { get; set; }

        public bool Niveladores { get; set; }

        public bool Cromado { get; set; }

        public bool Zincado { get; set; }

        public bool NingunAccesorio { get; set; }

        public string? ComentariosAdicionales { get; set; }


        public string? Item1name { get; set; }

        public string? Item1Alto { get; set; }

        public string? Item1Ancho { get; set; }


        public string? Item2name { get; set; }

        public string? Item2Alto { get; set; }

        public string? Item2Ancho { get; set; }


        public string? Item3name { get; set; }

        public string? Item3Alto { get; set; }

        public string? Item3Ancho { get; set; }


        public string? Item4name { get; set; }

        public string? Item4Alto { get; set; }

        public string? Item4Ancho { get; set; }


        [NotMapped]
        public IFormFile? FilePieza { get; set; }

        public string? ArchivoPieza { get; set; }


        public ICollection<Brief>? Briefs { get; set; }
    }
}

