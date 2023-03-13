using Core.Convertors;
using Core.DTOs;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.User
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserForAdminViewModel CreateUser { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            #region VALIDATION

            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                ViewData["IsSuccess"] = false;
                return Page();
            }
            if (_userService.IsExistUserName(CreateUser.UserName))
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                ViewData["IsSuccess"] = false;
                ModelState.AddModelError("CreateUser.UserName", "Duplicate username !");
                return Page();
            }
            if (_userService.IsExistEmail(FixedText.FixedEmail(CreateUser.Email)))
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                ViewData["IsSuccess"] = false;
                ModelState.AddModelError("CreateUser.Email", "The user's email is duplicate !");
                return Page();
            }

            #endregion

            int userId = _userService.AddUserFromAdmin(CreateUser);

            //ADD USER ROLES
            _permissionService.AddRolesToUser(SelectedRoles, userId);


            // REDIRECT TO ACCOUNT CONTROLLER FOR SEND EMAIL
            if (CreateUser.IsActive == 3)
            {
                return RedirectToAction("AddUserFromAdmin", "Account", new { id = userId });
            }

            return Redirect("/Admin/User");
        }
    }
}
