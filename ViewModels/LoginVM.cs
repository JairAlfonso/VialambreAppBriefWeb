using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VialambreAppTest1.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El correo es necesario")]
        [EmailAddress]
        [Display(Name = "Correo:")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es necesaria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener minimo 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        public string? Password { get; set; }

        [NotMapped]
        [Display(Name = "Recuerdame:")]
        public bool RememberMe { get; set; }
    }
}
