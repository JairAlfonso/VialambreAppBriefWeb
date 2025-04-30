using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;
namespace VialambreAppTest1.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _notify;
        private readonly IMapper _mapper;

        [BindProperty]
        public UserInputVM? UserInputVM { get; set; }

        public RegisterModel(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IToastNotification notify, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notify = notify;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            if (!await _roleManager.RoleExistsAsync(RolesApp.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.Admin));
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.AdminUser));
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.Costos));
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.Design));
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.Asesor));
                await _roleManager.CreateAsync(new IdentityRole(RolesApp.Client));
            }
        }

        public async Task<JsonResult> OnGetCheckEmail(UserInputVM userInputVM)
        {
            var user = await _userManager.FindByEmailAsync(userInputVM.Email);
            bool valid = (user == null);
            return new JsonResult(valid);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                AppUser user = _mapper.Map<AppUser>(UserInputVM);
                user.UserName = UserInputVM!.Email;
                string role = Request.Form["rUserRole"].ToString();
                string roleToAdd = role != "" ? role : RolesApp.Client;
                await _userManager.CreateAsync(user, UserInputVM.Password);
                // Guardar el rol con el usuario 
                user.UserId = user.Id;
                user.RolName = roleToAdd;
                user.CreateDate = DateTime.Now;
                await _userManager.AddToRoleAsync(user, roleToAdd);
                _notify.AddSuccessToastMessage("El usuario se creó correctamente");
                return RedirectToPage("Login");
            }
            else
            {
                _notify.AddErrorToastMessage("El usuario NO se creó!!");
            }

            return Page();
        }
    }
}
