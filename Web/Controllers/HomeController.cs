using Core.Servises;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private ICourseService _courseService;
        public HomeController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            ViewBag.LastestCourse = _courseService.GetLastestCourse();
            ViewBag.PopularCourse = _courseService.GetPopularCourse();
            return View();
        }


        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletByWalletId(id);


                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }

            return View();
        }


        public JsonResult GetSubGroups(int id)
        {
            List<SelectListItem> subGroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text="Selected",Value="" }
            }; subGroups.AddRange(_courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(subGroups, "Value", "Text"));
        }
    }
}
