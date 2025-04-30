using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly ViAppContext _context;

        public EditModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product? Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = _context.Product.FirstOrDefault(p => p.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productDb = await _context.Product.FindAsync(Product!.Id);
            if (productDb == null)
            {
                return NotFound();
            }

            productDb.Name = Product.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Manejo de errores (log, mostrar un mensaje al usuario, etc.)
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los cambios. Intente de nuevo.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
