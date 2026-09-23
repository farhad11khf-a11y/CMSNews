

using CMSNews.Models.ViewModels;
using CMSNews.Service;
using CMSNews.Models.Context;
using System.Web.Mvc;
using System.Web.Security;
using CMSNews.Service.Service;
using System.Linq;

namespace CMSNews.Controllers
{
    public class AccountController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext ();
        UserService _userService;

        public AccountController()
        {
            _userService = new UserService(db);
        }
        public ActionResult Login(string returnUrl = "/")
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        // GET: Account/Login
    

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAll().
                    FirstOrDefault(t => t.MobileNumber == model.MobileNumber && t.Password == model.Password);
                if (user!=null  )
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(model.MobileNumber, model.RememberMe);

                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(
                    "MobileNumber",
                    "حساب کاربری شما فعال نمی باشد "
                );
                }

                ModelState.AddModelError(
                    "MobileNumber",
                    "نام کاربری یا رمز عبور اشتباه است"
                );
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}



