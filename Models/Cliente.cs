using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Producto:")]
        public string? Name { get; set; }

        [Display(Name = "Documento:")]
        public string? Document { get; set; }

        public ICollection<Brief>? Briefs { get; set; }
    }
}
