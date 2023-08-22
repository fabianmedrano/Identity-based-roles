using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Rol
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;

        }

        [BindProperty]
        public IdentityRole Role { set; get; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Role = await _roleManager.FindByIdAsync(id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) return Page();

            var roleToUpdate = new IdentityRole
            {
                Id = Role.Id,
                Name = Role.Name
            };
            var result = await _roleManager.UpdateAsync(roleToUpdate);

            if (!result.Succeeded) return Page();

            return RedirectToPage("./Index");
        }
    }
}
