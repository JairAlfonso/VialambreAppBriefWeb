using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VialambreAppTest1.ViewModels
{
    public class UserInputVM
    {
        [Required(ErrorMessage = "El documento es necesario")]
        [Display(Name = "Documento")]
        public string? Document { get; set; }

        [Required(ErrorMessage = "El nombre es necesario")]
        [Display(Name = "Nombre")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [Display(Name = "Apellido")]
        public string? LastName { get; set; }

        [Display(Name = "Nombre")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El correo es necesario")]
        [EmailAddress]
        
        [Display(Name = "Correo")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "La contraseña es necesaria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener minimo 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirma la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar contraseña")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El teléfono es necesario")]
        [StringLength(10, ErrorMessage = "El teléfono debe contener {1} dígitos")]
        [Display(Name = "Teléfono")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Rol")]
        public string? RolName { get; set; }

        [Display(Name = "Fecha Creado")]
        public string? CreateDate { get; set; }
    }
}
