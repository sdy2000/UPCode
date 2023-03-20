using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permission;
        public CreateRoleModel(IPermissionService permission)
        {
            _permission = permission;
        }

        [BindProperty]
        public DataLayer.Entities.User.Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permission.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (string.IsNullOrEmpty(Role.RoleTitle))
            {
                ViewData["Permissions"] = _permission.GetAllPermissions();
                ModelState.AddModelError("Role.RoleTitle", "Role title is required !");
                return Page();
            }
            if (_permission.IsExistRole(Role.RoleTitle))
            {
                ViewData["Permissions"] = _permission.GetAllPermissions();
                ModelState.AddModelError("Role.RoleTitle", "Role Title is repeated !");
                return Page();
            }

            int roleId = _permission.AddRole(Role);

            // ADD ROLE PERMISSION
            _permission.AddPermissionToRole(SelectedPermission, roleId);

            return Redirect("/Admin/Role");
        }
    }
}
