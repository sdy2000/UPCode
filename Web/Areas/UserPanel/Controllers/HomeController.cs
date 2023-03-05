using Core.Convertors;
using Core.DTOs;
using Core.Senders;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
            EditProfileViewModel userInfo = _userService.GetDataForEditProfileUser(User.Identity.Name);

            if (user == null)
                return BadRequest();

            List<SelectListItem> genders = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Select Gender",Value="0"}
            }; genders.AddRange(_userService.GetGenderForEditUser());
            ViewData["Genders"] = new SelectList(genders, "Value", "Text", userInfo.GenderId);

            return View();
        }


        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel editProfile)
        {
            EditProfileViewModel userInfo = _userService.GetDataForEditProfileUser(User.Identity.Name);

            if (!ModelState.IsValid)
            {

                List<SelectListItem> genders = new List<SelectListItem>()
                    {
                        new SelectListItem(){Text="انتخاب کنید",Value="0"}
                    }; genders.AddRange(_userService.GetGenderForEditUser());
                ViewData["Genders"] = new SelectList(genders, "Value", "Text", userInfo.GenderId);
                return View(editProfile);
            }

            return Redirect("/Login?EditProfile=true");


            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _userService.EditProfile(editProfile, User.Identity.Name);

            return Redirect("/UserPanel");
        }

        #endregion
    }
}
