using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly ViAppContext _context;

        public CreateModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product? Product { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Product!);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Briefs/Admin/Products/Index");
        }
    }
}
