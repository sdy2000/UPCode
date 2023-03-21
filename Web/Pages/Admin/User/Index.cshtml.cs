using Core.DTOs;
using Core.Security;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.User
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserForAdminViewModel UserForAdminViewModel { get; set; }
        public void OnGet(int pageId = 1, string userNameFilter = "", string emailFilter = "",
            int genderId = 0, int take = 10)
        {
            ViewData["pageId"] = pageId;
            UserForAdminViewModel = _userService.GetUserForAdmin(pageId, userNameFilter, emailFilter, genderId, take);
        }
    }
}
