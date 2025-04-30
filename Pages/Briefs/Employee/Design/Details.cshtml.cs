using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Pages.Briefs.Employee.Design
{
    public class DetailsModel : PageModel
    {
        private readonly BriefService _briefService;

        public List<Pieza>? PiezasAsociadas { get; set; }

        public IEnumerable<BriefVM>? BriefVM { get; set; }
        public IEnumerable<Brief>? DesignBriefs { get; set; }

        [BindProperty]
        public Brief? BriefDetails { get; set; }

        [BindProperty]
        public LoginVM? LoginVM { get; set; }

        public DetailsModel(BriefService briefService)
        {
            _briefService = briefService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (!User.IsInRole("Design"))
            {
                return RedirectToPage("/Users/AccessDenied");
            }

            BriefDetails = await _briefService.GetBriefDetailsAsync(id);

            if (BriefDetails == null)
            {
                // Maneja el caso en el que el brief no se encuentre
                return NotFound();
            }

            PiezasAsociadas = await _briefService.GetPiezasAsync(id);

            return Page();
        }
    }
}