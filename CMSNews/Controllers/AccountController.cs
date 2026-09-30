

using CMSNews.Models.ViewModels;
using CMSNews.Service;
using CMSNews.Models.Context;
using System.Web.Mvc;
using System.Web.Security;
using CMSNews.Service.Service;
using System.Linq;
using System;
using CMSNews.Models.Models;
using CMSNews.App_Start;
using System.IO;
using System.Web;

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


        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAll()
                    .FirstOrDefault(u => u.MobileNumber == model.MobileNumber);

                if (user != null)
                {
                    ModelState.AddModelError(
                        "MobileNumber",
                        "این شماره موبایل قبلاً ثبت شده است");

                    return View(model);
                }

                //var newUser = AutoMapperConfig.mapper.Map<RegisterViewModel, User>(model);
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNumber = model.MobileNumber,
                    Password = model.Password,
                    RegisteDate = DateTime.Now,
                    IsActive = true,
                    Role = "User"
                };

                _userService.Add(newUser);
                _userService.Save();

                FormsAuthentication.SetAuthCookie(
                    newUser.MobileNumber,
                    false);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        [Authorize]
        public ActionResult myProfile()
        {
            var user = _userService.GetAll()
                .FirstOrDefault(x => x.MobileNumber == User.Identity.Name);

            if (user == null)
            {
                return HttpNotFound();
            }
            var model = AutoMapperConfig.mapper
          .Map<ProfileViewModel>(user);

            return View(model);
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            var user = _userService.GetAll()
                .FirstOrDefault(x => x.MobileNumber == User.Identity.Name);

            if (user == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.mapper
                .Map<ProfileViewModel>(user);

            return View(model);
        }

        // POST: Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditProfile(
            ProfileViewModel model,
            HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // پیدا کردن کاربر فعلی
            var user = _userService.GetAll()
                .FirstOrDefault(x => x.MobileNumber == User.Identity.Name);

            if (user == null)
            {
                return HttpNotFound();
            }

            // اگر کاربر عکس جدید انتخاب کرده باشد
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                // گرفتن پسوند فایل
                string extension = Path.GetExtension(ImageFile.FileName)
                    .ToLower();

                // پسوندهای مجاز
                string[] allowedExtensions =
                {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif"
        };

                // بررسی پسوند
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(
                        "ImageFile",
                        "فرمت تصویر مجاز نیست");

                    return View(model);
                }

                // بررسی حجم؛ حداکثر 2 مگابایت
                if (ImageFile.ContentLength > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError(
                        "ImageFile",
                        "حجم تصویر نباید بیشتر از 2 مگابایت باشد");

                    return View(model);
                }

                // مسیر پوشه تصاویر کاربران
                string folderPath = Server.MapPath(
                    "~/Images/users/");

                // اگر پوشه وجود نداشت، ایجاد شود
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // ساخت نام یکتا برای عکس
                string newFileName =
                    Guid.NewGuid().ToString("N") + extension;

                // مسیر کامل ذخیره عکس
                string newFilePath =
                    Path.Combine(folderPath, newFileName);

                // ذخیره عکس
                ImageFile.SaveAs(newFilePath);

                // حذف عکس قبلی، اگر nophoto.png نباشد
                if (!string.IsNullOrEmpty(user.ImageName) &&
                    user.ImageName != "nophoto.png")
                {
                    string oldFilePath =
                        Path.Combine(folderPath, user.ImageName);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // ثبت نام عکس جدید در دیتابیس
                user.ImageName = newFileName;
            }

            // بروزرسانی اطلاعات کاربر
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            _userService.Update(user);
            _userService.Save();

            return RedirectToAction("MyProfile");
        }

        public ActionResult Login(string returnUrl = "/")
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        

    

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

            return Redirect("/");
        }
    }
}



