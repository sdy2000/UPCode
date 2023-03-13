using Core.DTOs.User;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.User
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserForAdminViewModel EditUser { get; set; }
        public void OnGet(int id)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            EditUser = _userService.GetUserForEditInAdmin(id);
        }
    }
}
