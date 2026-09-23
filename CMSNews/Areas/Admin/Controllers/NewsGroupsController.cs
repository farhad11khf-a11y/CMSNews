using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using CMSNews.Service.Service;
using CMSNews.Repository.Repository;
using CMSNews.App_Start;
using System.Collections.Generic;
using CMSNews.Models.ViewModels;
using System.Web;
using System;
using System.IO;
using System.Linq;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewsGroupsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        NewsGroupService _newsGroupService;
        public NewsGroupsController()
        {
            _newsGroupService = new NewsGroupService(db);
        }

        // GET: Admin/NewsGroups
        public ActionResult Index()
        {
            var newsGroups = _newsGroupService.GetAll();

            List<NewsGroupsViewModel> newsGroupsViewModels =
              AutoMapperConfig.mapper.Map<IEnumerable<NewsGroup>, List<NewsGroupsViewModel>>(newsGroups);
            return View(newsGroupsViewModels);
        }

        // GET: Admin/NewsGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);

            NewsGroupsViewModel newsGroupsViewModel = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupsViewModel>(newsGroup);
            if (newsGroup == null)
            {
                return HttpNotFound();
            }
            return View(newsGroupsViewModel);
        }

        // GET: Admin/NewsGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsGroupTitle")]
        NewsGroupsViewModel newsGroupsViewModel, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {

                string imageName = "nophoto.png";

                if (imgUpload != null)
                {
                    // بررسی پسوند
                    string extension = Path.GetExtension(imgUpload.FileName).ToLower();

                    string[] allowedExtensions =
                    {
                        ".jpg",
                        ".jpeg",
                        ".png",
                        ".gif"
                    };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "فرمت تصویر مجاز نیست."
                        );

                        return View(newsGroupsViewModel);
                    }

                    // بررسی حجم فایل
                    int maxSize = 2 * 1024 * 1024; // 2 MB

                    if (imgUpload.ContentLength > maxSize)
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "حجم تصویر نباید بیشتر از 2 مگابایت باشد."
                        );

                        return View(newsGroupsViewModel);
                    }

                    // ساخت نام جدید
                    imageName = Guid.NewGuid().ToString().Replace("-", "")
                                 + extension;

                    // ذخیره تصویر
                    imgUpload.SaveAs(
                        Server.MapPath("~/Images/news.group/") + imageName
                    );
                }




                newsGroupsViewModel.ImageName = imageName;
                newsGroupsViewModel.NewsGroupId = _newsGroupService.NextNewsGroupId();
                NewsGroup newsGroup = AutoMapperConfig.mapper.Map<NewsGroupsViewModel, NewsGroup>(newsGroupsViewModel);
                _newsGroupService.Add(newsGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }

            return View(newsGroupsViewModel);
        }

        // GET: Admin/NewsGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);
            if (newsGroup == null)
            {
                return HttpNotFound();
            }
            NewsGroupsViewModel newsGroupsViewModel = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupsViewModel>(newsGroup);

            return View(newsGroupsViewModel); 
        }

        // POST: Admin/NewsGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsGroupId,NewsGroupTitle,ImageName")]
                                            NewsGroupsViewModel newsGroupsViewModel, HttpPostedFileBase imgUpload)
        {
            #region کد اصلاح شده
            if (ModelState.IsValid)
            {
                if (imgUpload != null)
                {
                    // بررسی پسوند
                    string extension =
                        Path.GetExtension(imgUpload.FileName).ToLower();

                    string[] allowedExtensions =
                    {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif"
            };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "فرمت تصویر مجاز نیست."
                        );

                        return View(newsGroupsViewModel);
                    }

                    // بررسی حجم
                    int maxSize = 2 * 1024 * 1024;

                    if (imgUpload.ContentLength > maxSize)
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "حجم تصویر نباید بیشتر از 2 مگابایت باشد."
                        );

                        return View(newsGroupsViewModel);
                    }

                    // حذف تصویر قبلی، فقط اگر پیش‌فرض نباشد
                    if (newsGroupsViewModel.ImageName != "nophoto.png")
                    {
                        System.IO.File.Delete(
                            Server.MapPath("~/Images/news.group/")
                            + newsGroupsViewModel.ImageName
                        );
                    }

                    // ساخت نام جدید
                    newsGroupsViewModel.ImageName =
                        Guid.NewGuid().ToString().Replace("-", "")
                        + extension;

                    // ذخیره تصویر جدید
                    imgUpload.SaveAs(
                        Server.MapPath("~/Images/news.group/")
                        + newsGroupsViewModel.ImageName
                    );
                }

                // بعد از تغییرات تصویر، تبدیل انجام شود
                NewsGroup newsGroup =
                    AutoMapperConfig.mapper.Map<NewsGroupsViewModel, NewsGroup>(
                        newsGroupsViewModel
                    );

                _newsGroupService.Update(newsGroup);
                _newsGroupService.Save();

                return RedirectToAction("Index");
            }
            #endregion

            #region اصلاح
            //if (ModelState.IsValid)
            //{

            //    NewsGroup newsGroup = AutoMapperConfig.mapper.Map<NewsGroupsViewModel, NewsGroup>(newsGroupsViewModel);
            //    if (imgUpload != null)
            //    {

            //        if (newsGroupsViewModel.ImageName != "nophoto.png")
            //        {
            //            System.IO.File.Delete(Server.MapPath("~/Images/news.group/") + newsGroupsViewModel.ImageName);


            //            // بررسی پسوند
            //            string extension = Path.GetExtension(imgUpload.FileName).ToLower();

            //            string[] allowedExtensions =
            //            {
            //            ".jpg",
            //            ".jpeg",
            //            ".png",
            //            ".gif"
            //        };

            //            if (!allowedExtensions.Contains(extension))
            //            {
            //                ModelState.AddModelError(
            //                    "ImageName",
            //                    "فرمت تصویر مجاز نیست."
            //                );

            //                return View(newsGroupsViewModel);
            //            }

            //            // بررسی حجم فایل
            //            int maxSize = 2 * 1024 * 1024; // 2 MB

            //            if (imgUpload.ContentLength > maxSize)
            //            {
            //                ModelState.AddModelError(
            //                    "ImageName",
            //                    "حجم تصویر نباید بیشتر از 2 مگابایت باشد."
            //                );

            //                return View(newsGroupsViewModel);
            //            }


            //        }
            //        else
            //        {
            //            // ساخت نام جدید
            //            string extension = Path.GetExtension(imgUpload.FileName).ToLower();
            //            newsGroupsViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "")
            //                         + extension;
            //        }


            //        // ذخیره تصویر
            //        imgUpload.SaveAs(
            //            Server.MapPath("~/Images/news.group/") + newsGroupsViewModel.ImageName
            //        );
            //    }

            //    _newsGroupService.Update(newsGroup);
            //    _newsGroupService.Save();
            //    return RedirectToAction("Index");
            //}
            #endregion



            return View(newsGroupsViewModel);
        }

        // GET: Admin/NewsGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);
            if (newsGroup == null)
            {
                return HttpNotFound();
            }
            NewsGroupsViewModel newsGroupsViewModel = AutoMapperConfig.mapper.Map<NewsGroup, NewsGroupsViewModel>(newsGroup);


            return View(newsGroupsViewModel);
        }

        // POST: Admin/NewsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            #region اصلاح شد
            var newsGroup = _newsGroupService.GetEntity(id);

            if (newsGroup == null)
            {
                return HttpNotFound();
            }

            _newsGroupService.Delete(id);
            _newsGroupService.Save();

            if (newsGroup.ImageName != "nophoto.png")
            {
                string imagePath =
                    Server.MapPath("~/Images/news.group/")
                    + newsGroup.ImageName;

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            #endregion
            #region اصلاح شه
            //var newsGroup = _newsGroupService.GetEntity(id);
            //_newsGroupService.Delete(id);
            //_newsGroupService.Save();
            //if (newsGroup.ImageName != "nophoto.png")
            //{
            //    System.IO.File.Delete(Server.MapPath("~/Images/news.group/") + newsGroup.ImageName);
            //}
            #endregion

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            _newsGroupService.Dispose();

        }
    }
}
