using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Empaques
{
    public class CreateModel : PageModel
    {
        private readonly ViAppContext _context;

        public CreateModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Empaque? Empaque { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Empaque!);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Briefs/Admin/Empaques/Index");
        }
    }
}
