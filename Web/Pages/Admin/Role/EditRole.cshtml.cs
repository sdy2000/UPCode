using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
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
    }
}
