using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IToastNotification _notify;
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public LoginModel(SignInManager<AppUser> signInManager, IToastNotification notify, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _notify = notify;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginVM!.Email, LoginVM.Password, LoginVM!.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(LoginVM.Email);
                    if (await _userManager.IsInRoleAsync(user, "Costos"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/Employee/Costos/Pendientes");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Design"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/Employee/Design/Pendientes");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/Admin/Index");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "AdminUser"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/AdminUser/Index");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Client"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/Client/Pendiente");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Asesor"))
                    {
                        _notify.AddSuccessToastMessage("Bienvenido " + user.FullName);
                        return RedirectToPage("/Briefs/Client/Pendientes");
                    }
                }
                else
                {
                    _notify.AddWarningToastMessage("Correo y/o contraseña NO válidos");
                    return Page();
                }
            }

            _notify.AddErrorToastMessage("Los datos de ingreso NO son correctos!!!");
            return Page();
        }

        public async Task<IActionResult> OnGetLogOut()
        {
            await _signInManager.SignOutAsync();
            _notify.AddSuccessToastMessage("La sesión finalizó correctamente");
            return RedirectToPage("/Users/Login");
        }
    }
}
