using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Servises.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Senders;

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

            IsRegisterViewModel isRegister = _userService.RegisterUser(register);

            if (!isRegister.IsSuccess)
            {
                if (isRegister.IsExistUserName)
                    ModelState.AddModelError("UserName", "Duplicate username!");
                
                if (isRegister.IsExistEmail)
                    ModelState.AddModelError("Email", "The entered email is duplicate!");
                
                if (!isRegister.IsSendActiovationEmail)
                    ModelState.AddModelError("Email", "There was a problem sending the activation email!");
                
                if (!isRegister.IsAddUser)
                    ModelState.AddModelError("Email", "An error has occurred, please try again later!");


                return View(register);
            }


            return View("_SuccessRegister", register);
        }

        #endregion


        #region LOGIN

        [Route("/Login")]
        public IActionResult Login(bool EditProfile = false, bool ChengPassword = false)
        {
            ViewBag.EditProfile = EditProfile;
            ViewBag.ChengPassword = ChengPassword;
            return View();
        }


        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Login = 3;
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);


                    ViewBag.Login = 1;
                    return View();
                }
                else
                {
                    ViewBag.Login = 2;
                    return View(login);
                }
            }

            ViewBag.Login = 3;
            return View(login);
        }

        #endregion


        #region LOGOUT

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion


        #region ACTIVE ACCOUNT

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion


        
    }
}
