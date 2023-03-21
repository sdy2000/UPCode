using Core.Convertors;
using Core.DTOs;
using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.User
{
    [PermissionChecker(5)]
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

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            var user = _userService.GetUserByUserId(EditUser.UserId);

            #region VALIDATION

            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                ViewData["IsSuccess"] = false;
                return Page();
            }
            if (user.UserName != EditUser.UserName)
            {
                if (_userService.IsExistUserName(EditUser.UserName))
                {
                    ViewData["Roles"] = _permissionService.GetAllRoles();
                    ViewData["IsSuccess"] = false;
                    ModelState.AddModelError("CreateUser.UserName", "Duplicate username!");
                    return Page();
                }
            }
            if (user.Email != FixedText.FixedEmail(EditUser.Email))
            {
                if (_userService.IsExistEmail(FixedText.FixedEmail(EditUser.Email)))
                {
                    ViewData["Roles"] = _permissionService.GetAllRoles();
                    ViewData["IsSuccess"] = false;
                    ModelState.AddModelError("CreateUser.Email", "The user's email is duplicate!");
                    return Page();
                }
            }

            #endregion

            int saveEdit = _userService.UpdateUserFromAdmin(EditUser);
            if(saveEdit==0)
            {

                ViewData["Roles"] = _permissionService.GetAllRoles();
                ViewData["IsSuccess"] = false;
                ModelState.AddModelError("CreateUser.UserName", "Some thing was rong !");
                return Page();
            }

            // UPDATE USER ROLES
            if(SelectedRoles!=null)
            _permissionService.EditUserRoles(SelectedRoles, user.UserId);


            // REDIRECT TO ACCOUNT CONTROLLER FOR SEND EMAIL
            if (EditUser.IsActive == 3)
            {
                return RedirectToAction("AddUserFromAdmin", "Account", new { id = user.UserId });
            }

            return Redirect("/Admin/User");
        }
    }
}
