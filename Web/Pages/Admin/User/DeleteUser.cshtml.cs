using Core.DTOs;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.User
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public DeleteUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }


        [BindProperty]
        public DeleteUserForAdminViewModel DeleteUser { get; set; }
        public void OnGet(int id)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            DeleteUser = _userService.GetUserForDelete(id);
        }

        public IActionResult OnPost()
        {
            bool isDeleteUser = _userService.DeleteUser(DeleteUser.UserId);

            if (!isDeleteUser)
            {
                ViewData["IsSuccess"] = isDeleteUser;
                return Page();
            }

            return Redirect("/Admin/User");
        }
    }
}
