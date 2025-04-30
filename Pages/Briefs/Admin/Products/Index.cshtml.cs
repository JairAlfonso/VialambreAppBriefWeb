using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ViAppContext _context;

        public IndexModel(ViAppContext context)
        {
            _context = context;
        }

        public IList<Product>? Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Product.ToListAsync();
        }

        public async Task<IActionResult> OnPostBorrar(int id)
        {

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
