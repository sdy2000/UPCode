using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
    [PermissionChecker(12)]
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public DataLayer.Entities.Users.Role Role { get; set; }
        public void OnGet(int id)
        {
            ViewData["PermissionTitle"] = _permissionService.GetPermissionTitleRole(id);
            Role = _permissionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            var role = _permissionService.GetRoleById(Role.RoleId);
            role.IsDelete = true;

            _permissionService.UpdateRole(role);

            return Redirect("/Admin/Role");
        }
    }
}
