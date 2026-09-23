using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class CommentsController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        CommentService _commentService;
        NewsService _newsService;
        public CommentsController()
        {
            _commentService = new CommentService(db);
            _newsService = new NewsService(db);
        }
        // GET: Admin/Comments
        public ActionResult Index()
        {
             var comments = _commentService.GetAll();
            List<CommentsViewMdel> commentViewModels =
                 AutoMapperConfig.mapper.Map<IEnumerable<Comment>, List<CommentsViewMdel>>(comments);
            return View(commentViewModels);
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //// GET: Admin/Comments/Create
        //public ActionResult Create()
        //{
        //    ViewBag.NewsId = new SelectList(db.News, "NewsId", "NewsTitle");
        //    return View();
        //}

        //// POST: Admin/Comments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CommentId,CommentText,Name,Email,RegisterDate,IsActive,NewsId")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Comments.Add(comment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.NewsId = new SelectList(db.News, "NewsId", "NewsTitle", comment.NewsId);
        //    return View(comment);
        //}

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment =_commentService.GetEntity(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsId = comment.News.NewsTitle;
            CommentsViewMdel commentViewModels =
               AutoMapperConfig.mapper.Map< Comment,CommentsViewMdel>(comment);
            //ViewBag.NewsId = new SelectList(_newsService.GetEntity(id.Value), "NewsId", "NewsTitle", comment.NewsId);
        
            return View(commentViewModels);
           
        
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,CommentText,Name,Email,RegisterDate,IsActive,NewsId")] CommentsViewMdel commentViewModels)
        {

            if (ModelState.IsValid)
            {
                Comment comment = AutoMapperConfig.mapper.Map<CommentsViewMdel, Comment>(commentViewModels);
                _commentService.Add(comment);
                _commentService.Save();
                return RedirectToAction("Index");
            }
        
            return View(commentViewModels);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
