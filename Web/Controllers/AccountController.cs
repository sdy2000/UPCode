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



        #endregion
    }
}
