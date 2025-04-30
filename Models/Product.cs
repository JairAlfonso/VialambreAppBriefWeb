using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de Producto es obligatorio")]
        [Display(Name = "Producto:")]
        public string? Name { get; set; }

        public ICollection<Brief>? Briefs { get; set; }
    }
}
