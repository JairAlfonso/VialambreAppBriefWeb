using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _notify;

        [BindProperty]
        public UserInputVM? UserInputVM { get; set; }

        public ChangePasswordModel(UserManager<AppUser> userManager, IToastNotification notify)
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
                    Email = user.Email
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

            if (UserInputVM == null || string.IsNullOrEmpty(UserInputVM.OldPassword) || string.IsNullOrEmpty(UserInputVM.Password))
            {
                ModelState.AddModelError(string.Empty, "Debes proporcionar la contraseña actual y la nueva contraseña.");
                return Page();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, UserInputVM.OldPassword, UserInputVM.Password);

            if (changePasswordResult.Succeeded)
            {
                _notify.AddSuccessToastMessage("La contraseña ha sido actualizada correctamente.");
                return RedirectToPage("/Users/Profile");
            }
            else
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
