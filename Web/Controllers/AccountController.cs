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
    }
}
