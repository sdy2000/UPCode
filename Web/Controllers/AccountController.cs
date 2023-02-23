using Core.DTOs;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {



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

            _userService.AddUser(user);


            #region SEND ACTIVATION EMAIL

            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی", body);

            #endregion



            return View("_SuccessRegister", user);
        }

        #endregion
    }
}
