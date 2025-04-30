using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.LugarEntregas
{
    public class IndexModel : PageModel
    {
        private readonly ViAppContext _context;

        public IndexModel(ViAppContext context)
        {
            _context = context;
        }

        public IList<LugarEntrega>? LugarEntrega { get; set; }

        public async Task OnGetAsync()
        {
            LugarEntrega = await _context.LugarEntrega.ToListAsync();
        }

        public async Task<IActionResult> OnPostBorrar(int id)
        {

            var lugarEntrega = await _context.LugarEntrega.FindAsync(id);
            if (lugarEntrega == null)
            {
                return NotFound();
            }

            _context.LugarEntrega.Remove(lugarEntrega);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
