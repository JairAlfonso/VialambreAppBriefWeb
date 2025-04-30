using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Empaques
{
    public class IndexModel : PageModel
    {
        private readonly ViAppContext _context;

        public IndexModel(ViAppContext context)
        {
            _context = context;
        }

        public IList<Empaque>? Empaques { get; set; }

        public async Task OnGetAsync()
        {
            Empaques = await _context.Empaque.ToListAsync();
        }

        public async Task<IActionResult> OnPostBorrar(int id)
        {

            var empaque = await _context.Empaque.FindAsync(id);
            if (empaque == null)
            {
                return NotFound();
            }

            _context.Empaque.Remove(empaque);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
