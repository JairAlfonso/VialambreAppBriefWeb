using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ViAppContext _context;

        public IndexModel(ViAppContext context)
        {
            _context = context;
        }

        public IList<Cliente>? Clientes { get; set; }

        public async Task OnGetAsync()
        {
            Clientes = await _context.Cliente.ToListAsync();
        }

        public async Task<IActionResult> OnPostBorrar(int id)
        {

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
