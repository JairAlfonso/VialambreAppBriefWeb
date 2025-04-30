using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Users
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;

        [BindProperty]
        public ResetPasswordVM ResetPasswordVM { get; set; }

        public ResetPasswordModel(UserManager<AppUser> userManager, IToastNotification notify)
        {
            _userManager = userManager;
            _notify = notify;
            ResetPasswordVM = new ResetPasswordVM();  // Inicializar la clave
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(ResetPasswordVM.Email);

                if (user == null)
                {
                    _notify.AddErrorToastMessage("El correo electrónico no está registrado.");
                    return Page();
                }

                // Genera el token de restablecimiento de contraseña
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Restablece la contraseña con el token generado
                var result = await _userManager.ResetPasswordAsync(user, resetToken, ResetPasswordVM.NewPassword);

                if (result.Succeeded)
                {
                    _notify.AddSuccessToastMessage("Contraseña restablecida correctamente.");
                    return RedirectToPage("/Users/Login");
                }
                else
                {
                    _notify.AddErrorToastMessage("No se pudo restablecer la contraseña.");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Page();
        }
    }
}
