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
    public class UsersController : Controller
    {
        private DbCMSNewsContext db = new DbCMSNewsContext();
        UserService _userService;
        public UsersController()
        {

            _userService = new UserService(db);
        }
        // GET: Users
        public ActionResult Index()
        {

            var user = _userService.GetAll();

            List<UserViewModel> userViewModels =
              AutoMapperConfig.mapper.Map<IEnumerable<User>, List<UserViewModel>>(user);
            return View(userViewModels);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntity(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
           UserViewModel userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            return View(userViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntity(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserViewModel userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            return View(userViewModel);

            
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,MobileNumber,Password,RegisteDate,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
