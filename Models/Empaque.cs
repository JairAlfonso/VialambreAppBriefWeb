using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.Models
{
    public class Empaque
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del empaque es obligatorio")]
        [Display(Name = "Empaque:")]
        public string? Name { get; set; }

        public ICollection<Brief>? Briefs { get; set; }
    }
}
