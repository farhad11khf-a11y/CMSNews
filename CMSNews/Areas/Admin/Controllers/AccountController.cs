using CMSNews.Models.Context;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMSNews.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        UserService _userService;

        public AccountController()
        {
            _userService = new UserService(db);
        }
        public ActionResult Login(string returnUrl = "/Admin")
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        // POST: Admin/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAll()
                    .FirstOrDefault(t =>
                        t.MobileNumber == model.MobileNumber &&
                        t.Password == model.Password);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError(
                            "MobileNumber",
                            "حساب کاربری شما فعال نمی باشد");

                        return View(model);
                    }

                    if (user.Role != "Admin")
                    {
                        ModelState.AddModelError(
                            "MobileNumber",
                            "شما دسترسی ورود به پنل مدیریت را ندارید");

                        return View(model);
                    }

                    FormsAuthentication.SetAuthCookie(
                        model.MobileNumber,
                        model.RememberMe);

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Default");
                }

                ModelState.AddModelError(
                    "MobileNumber",
                    "نام کاربری یا رمز عبور اشتباه است");
            }

            return View(model);
        }


        // GET: Admin/Account/Logout
        public ActionResult Logout()
        {
            // حذف کوکی احراز هویت
            // بعد از این دستور، مدیر دیگر وارد سیستم محسوب نمی‌شود.
            FormsAuthentication.SignOut();

            // بعد از خروج، مدیر را به صفحه Login پنل مدیریت می‌فرستیم.
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}