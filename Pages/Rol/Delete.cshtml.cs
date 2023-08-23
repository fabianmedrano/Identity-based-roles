using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Identity.Pages.Rol
{
    public class DeleteModel : PageModel
    {

        private RoleManager<IdentityRole> _roleManager;

        public DeleteModel(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        [BindProperty]
        public IdentityRole Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id) {

            var rol = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == Role.Id);

            if (rol == null){
                return NotFound();
            }else { 
                var result =  await  _roleManager.DeleteAsync(rol);
            }

            return RedirectToPage("./Index");
        }
    }
}
