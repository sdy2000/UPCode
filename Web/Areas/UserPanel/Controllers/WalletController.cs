using Core.DTOs;
using Core.Servises.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }


        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
                return View();
            }
            int walletId = _userService.ChargeWallet(User.Identity.Name, charge.Amount, "Charge account");

            #region ONLINE PAYMENT

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var res = payment.PaymentRequest("Charge Wallet", "https://localhost:5074/OnlinePayment/" + walletId);
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion

            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }
    }
}
