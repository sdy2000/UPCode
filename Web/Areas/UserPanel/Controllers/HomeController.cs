using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        #region EDIT PROFILE

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var user = _userService.GetDataForEditProfileUser(User.Identity.Name);

            if (user == null)
                return BadRequest();

            List<SelectListItem> genders = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Select Gender",Value="0"}
            }; genders.AddRange(_userService.GetGenderForEditUser());
            ViewData["Genders"] = new SelectList(genders, "Value", "Text", user.GenderId);

            return View();
        }


        #endregion
    }
}
