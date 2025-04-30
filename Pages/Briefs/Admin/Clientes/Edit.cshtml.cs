using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Clientes
{
    public class EditModel : PageModel
    {
        private readonly ViAppContext _context;

        public EditModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente? Cliente { get; set; }

        public IActionResult OnGet(int id)
        {
            Cliente = _context.Cliente.FirstOrDefault(c => c.Id == id);
            if (Cliente == null)
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

            var clienteDb = await _context.Cliente.FindAsync(Cliente!.Id);
            
            if (clienteDb == null)
            {
                return NotFound();
            }

            clienteDb.Name = Cliente.Name;
            clienteDb.Document = Cliente.Document;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception )
            {
                // Manejo de errores (log, mostrar un mensaje al usuario, etc.)
                
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los cambios. Intente de nuevo.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
