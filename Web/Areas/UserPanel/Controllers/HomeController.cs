using Core.DTOs;
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

            if (userInfo == null)
                return BadRequest();

            List<SelectListItem> genders = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Select Gender",Value="0"}
            }; genders.AddRange(_userService.GetGenderForEditUser());
            ViewData["Genders"] = new SelectList(genders, "Value", "Text", userInfo.GenderId);

            return View(userInfo);
        }


        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel editProfile)
        {
            EditProfileInfoViewModel editInfo = _userService.EditProfile(editProfile, User.Identity.Name);
            EditProfileViewModel userInfo = _userService.GetDataForEditProfileUser(User.Identity.Name);


            if (!editInfo.IsSuccess || !ModelState.IsValid || string.IsNullOrEmpty(editProfile.Email))
            {

                if (editInfo.IsEmailExist)
                    ModelState.AddModelError("Email", "The entered email is available !");
                else if (editInfo.IsSendActiveEmail)
                    ModelState.AddModelError("Email", "Error sending activation email !");
                else
                    ModelState.AddModelError("Email", "Error Edit Profil");

                List<SelectListItem> genders = new List<SelectListItem>()
                    {
                        new SelectListItem(){Text="Select Gender",Value="0"}
                    }; genders.AddRange(_userService.GetGenderForEditUser());
                ViewData["Genders"] = new SelectList(genders, "Value", "Text", userInfo.GenderId);
                return View(editProfile);
            }


            if (editInfo.IsSendActiveEmail && editInfo.IsSuccess)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/Login?EditProfile=true");
            }



            return Redirect("/UserPanel");
        }

        #endregion

        #region EDIT PASSWORD

        [Route("UserPanel/EditPassword")]
        public IActionResult EditPassword()
        {

            return View();
        }

        #endregion
    }
}
