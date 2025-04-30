using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Pages.Briefs.Admin.Empaques
{
    public class EditModel : PageModel
    {
        private readonly ViAppContext _context;

        public EditModel(ViAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Empaque? Empaque { get; set; }

        [TempData]
        public string? Message { get; set; }

        public IActionResult OnGet(int id)
        {
            Empaque = _context.Empaque.FirstOrDefault(c => c.Id == id);
            if (Empaque == null)
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

            var empaquesDb = await _context.Empaque.FindAsync(Empaque!.Id);
            if (empaquesDb == null)
            {
                return NotFound();
            }

            empaquesDb.Name = Empaque.Name;

            try
            {
                await _context.SaveChangesAsync();
                Message = "Empaque actualizado exitosamente.";
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los cambios. Intente de nuevo.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}


