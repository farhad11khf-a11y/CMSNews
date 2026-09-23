using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSNews.App_Start;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
       NewsService _newsService;
        NewsGroupService _newsGroupService;
        UserService _userService;

        public NewsController()
        {
            _newsService =new NewsService(db);
            _newsGroupService = new NewsGroupService(db);
            _userService = new UserService(db);
        }

         
        // GET: Admin/News
        public ActionResult Index()
        {
            var news = _newsService.GetAll();

            List<NewsViewModel> newsViewModels =
              AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(news);
            return View(newsViewModels);
            //var news = db.News.Include(n => n.NewsGroup).Include(n => n.User);
            //return View(news.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //News news = _newsService.GetEntity(id.Value);
            News news = _newsService.GetNewsWithDetails(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");
            //ViewBag.UserId = new SelectList(db.Users, "UserId", "MobileNumber");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,Description,NewsGroupId")]
                        NewsViewModel newsViewModel , HttpPostedFileBase imgUpload)
        {

            if (ModelState.IsValid)
            {
                #region save image to server

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

                        return View(newsViewModel);
                    }

                    // بررسی حجم فایل
                    int maxSize = 2 * 1024 * 1024; // 2 MB

                    if (imgUpload.ContentLength > maxSize)
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "حجم تصویر نباید بیشتر از 2 مگابایت باشد."
                        );

                        return View(newsViewModel);
                    }

                    // ساخت نام جدید
                    imageName = Guid.NewGuid().ToString().Replace("-", "")
                                 + extension;

                    // ذخیره تصویر
                    imgUpload.SaveAs(
                        Server.MapPath("~/Images/news/") + imageName
                    );
                }
                #endregion
                newsViewModel.ImageName = imageName;
                News  news  = AutoMapperConfig.mapper.Map<NewsViewModel, News >(newsViewModel);
                news.IsActive = true;
                news.see = 0;
                news.Like = 0;
                news.RegisterDate = DateTime.Now;
                //بیلیط User.Identity.Name
                news.UserId = _userService.GetUserId(User.Identity.Name);
                _newsService.Add(news);
                //db.News.Add(news);
                _newsService.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NewsGroupId = new SelectList(db.NewsGroups, "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "MobileNumber", newsViewModel.UserId);
            return View(newsViewModel);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
         
            return View(newsViewModel);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,Description,ImageName,RegisterDate,IsActive,see," +
            "Like,NewsGroupId,UserId")] NewsViewModel newsViewModel , HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                #region Edit Then save
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

                        return View(newsViewModel);
                    }

                    // بررسی حجم
                    int maxSize = 2 * 1024 * 1024;

                    if (imgUpload.ContentLength > maxSize)
                    {
                        ModelState.AddModelError(
                            "ImageName",
                            "حجم تصویر نباید بیشتر از 2 مگابایت باشد."
                        );

                        return View(newsViewModel);
                    }

                    // حذف تصویر قبلی، فقط اگر پیش‌فرض نباشد
                    if (newsViewModel.ImageName != "nophoto.png")
                    {
                        string imagePath =
                Server.MapPath("~/Images/news/")
                            + newsViewModel.ImageName;

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                     
                       
                    }

                    // ساخت نام جدید
                    newsViewModel.ImageName =
                        Guid.NewGuid().ToString().Replace("-", "")
                        + extension;
                  
                    // ذخیره تصویر جدید
                    imgUpload.SaveAs(
                        Server.MapPath("~/Images/news/")
                        + newsViewModel.ImageName
                    );
                }
                #endregion
                News news = AutoMapperConfig.mapper.Map<NewsViewModel, News>(newsViewModel);


                _newsService.Update(news);
                _newsService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle", newsViewModel.NewsGroupId);
        
            return View(newsViewModel);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //News news = db.News.Find(id);
            News news = _newsService.GetNewsWithDetails(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            NewsViewModel newsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _newsService.GetEntity(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            //db.News.Remove(news);
            _newsService.Delete(id);

            //db.SaveChanges();
            _newsService.Save();

            if (news.ImageName != "nophoto.png")
            {
                string imagePath =
                    Server.MapPath("~/Images/news/")
                    + news.ImageName;

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

                return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing) 
        {
            if (disposing)
    {
        _newsService.Dispose();
        _newsGroupService.Dispose();
        _userService.Dispose();
    }

    base.Dispose(disposing);
        }
    }
}
