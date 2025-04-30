using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.LugarEntregas
{
    public class EditModel : PageModel
    {
        private readonly ViAppContext _context;

        public EditModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LugarEntrega? LugarEntrega { get; set; }

        public IActionResult OnGet(int id)
        {
            LugarEntrega = _context.LugarEntrega.FirstOrDefault(c => c.Id == id);
            if (LugarEntrega == null)
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

            var lugarEntregaDb = await _context.LugarEntrega.FindAsync(LugarEntrega!.Id);
            if (lugarEntregaDb == null)
            {
                return NotFound();
            }

            lugarEntregaDb.Name = LugarEntrega.Name;

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
