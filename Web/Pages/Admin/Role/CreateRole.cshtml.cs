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
    }
}
