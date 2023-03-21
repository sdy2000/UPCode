using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Role
{
    [PermissionChecker(9)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permission;
        public IndexModel(IPermissionService permission)
        {
            _permission = permission;
        }

        public List<DataLayer.Entities.User.Role> RoleList { get; set; }
        public void OnGet(string userRoleFilter)
        {
            RoleList = _permission.GetAllRoles(userRoleFilter);
        }
    }
}
