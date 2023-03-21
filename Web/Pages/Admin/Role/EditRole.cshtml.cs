using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
    [PermissionChecker(11)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permission;
        public EditRoleModel(IPermissionService permission)
        {
            _permission = permission;
        }

        [BindProperty]
        public DataLayer.Entities.User.Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permission.GetRoleById(id);
            ViewData["Permissions"] = _permission.GetAllPermissions();
            ViewData["RolePermissions"] = _permission.GetPermissionRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            var role = _permission.GetRoleById(Role.RoleId);

            if (string.IsNullOrEmpty(Role.RoleTitle))
            {
                ViewData["Permissions"] = _permission.GetAllPermissions();
                ViewData["RolePermissions"] = _permission.GetPermissionRole(Role.RoleId);
                ModelState.AddModelError("Role.RoleTitle", "Role Title is requierd !");
                return Page();
            }
            if (role.RoleTitle != Role.RoleTitle)
            {
                if (_permission.IsExistRole(Role.RoleTitle))
                {
                    ViewData["Permissions"] = _permission.GetAllPermissions();
                    ViewData["RolePermissions"] = _permission.GetPermissionRole(Role.RoleId);
                    ModelState.AddModelError("Role.RoleTitle", "Role title is repited !");
                    return Page();
                }
            }

            _permission.UpdateRole(Role);

            // UPDATE ROLE PERMISSION
            _permission.EditPermissionToRole(SelectedPermission, Role.RoleId);

            return Redirect("/Admin/Role");
        }
    }
}
