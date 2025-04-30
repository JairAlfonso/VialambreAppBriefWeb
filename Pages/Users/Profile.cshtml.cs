using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Users
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;

        [BindProperty]
        public UserInputVM? UserInputVM { get; set; }

        public ProfileModel(UserManager<AppUser> userManager, IToastNotification notify)
        {
            _userManager = userManager;
            _notify = notify;
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                UserInputVM = new UserInputVM
                {
                    RolName = user.RolName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Document = user.Document,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    // CreateDate = user.CreateDate
                };
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["WarningMessage"] = "El usuario no está registrado.";
                return RedirectToPage("/Users/Login");
            }

            if (UserInputVM == null)
            {
                ModelState.AddModelError(string.Empty, "Los datos del usuario no están disponibles.");
                return Page();
            }

            // Validar el nuevo correo electrónico
            if (!string.IsNullOrEmpty(UserInputVM.Email) && UserInputVM.Email != user.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(UserInputVM.Email);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    ModelState.AddModelError("UserInputVM.Email", "Este correo ya está registrado.");
                    return Page();
                }
            }

            // Actualiza los campos del usuario
            user.UserName = UserInputVM.Email;
            user.RolName = UserInputVM.RolName;
            user.FirstName = UserInputVM.FirstName;
            user.LastName = UserInputVM.LastName;
            user.Document = UserInputVM.Document;
            user.Email = UserInputVM.Email;
            user.PhoneNumber = UserInputVM.PhoneNumber;
            user.CreateDate = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Tu perfil ha sido actualizado correctamente.";
                return RedirectToPage("/Users/Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
