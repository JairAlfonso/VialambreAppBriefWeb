using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "El correo es necesario")]
        [EmailAddress(ErrorMessage = "Debe ser una dirección de correo válida")]
        [Display(Name = "Correo Electrónico")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Ingresa la nueva contraseña")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirma la nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string? ConfirmPassword { get; set; }
    }
}