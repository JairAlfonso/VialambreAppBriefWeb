using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.Models
{
    public class LugarEntrega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre para el lugar de entrega es obligatorio")]
        [Display(Name = "Entregar en:")]
        public string? Name { get; set; }

               
       

        public ICollection<Brief>? Briefs { get; set; }
    }
}
