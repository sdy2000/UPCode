using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public DataLayer.Entities.User.Role Role { get; set; }
        public void OnGet(int id)
        {
            ViewData["PermissionTitle"] = _permissionService.GetPermissionTitleRole(id);
            Role = _permissionService.GetRoleById(id);
        }
    }
}
