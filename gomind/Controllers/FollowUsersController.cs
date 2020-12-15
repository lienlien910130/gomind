using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using gomind.Models;

namespace gomind.Controllers
{
    public class FollowUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FollowUsers
        public ActionResult Index()
        {
            var followUser = db.FollowUser.Include(f => f.User);
            return View(followUser.ToList());
        }

        // GET: FollowUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUser followUser = db.FollowUser.Find(id);
            if (followUser == null)
            {
                return HttpNotFound();
            }
            return View(followUser);
        }

        // GET: FollowUsers/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: FollowUsers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number,userid,username,ImageUrl,followuserid")] FollowUser followUser)
        {
            if (ModelState.IsValid)
            {
                db.FollowUser.Add(followUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

  
            return View(followUser);
        }

        // GET: FollowUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUser followUser = db.FollowUser.Find(id);
            if (followUser == null)
            {
                return HttpNotFound();
            }
        
            return View(followUser);
        }

        // POST: FollowUsers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number,userid,username,ImageUrl,followuserid")] FollowUser followUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(followUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
      
            return View(followUser);
        }

        // GET: FollowUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUser followUser = db.FollowUser.Find(id);
            if (followUser == null)
            {
                return HttpNotFound();
            }
            return View(followUser);
        }

        // POST: FollowUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FollowUser followUser = db.FollowUser.Find(id);
            db.FollowUser.Remove(followUser);
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
