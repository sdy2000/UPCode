using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }



        #region REGISTER

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }



        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid || register.Password != register.RePassword)
                return View(register);

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری تکراری میباشد !");
                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده تکراری میباشد !");
            }

            var user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                ActiveCode = NameGenerator.GeneratorUniqCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserAvatar = "No-Photo.jpg"
            };

            // TODO : Add User


            #region SEND ACTIVATION EMAIL

            // TODO : Send Activation Email

            #endregion


            // TODO Add _SuccessRegister View
            return View("_SuccessRegister", user);
        }

        #endregion
    }
}
