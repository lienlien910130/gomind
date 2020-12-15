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
using Microsoft.AspNet.Identity;

namespace gomind.Controllers
{
    public class FollowTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FollowTables
        public ActionResult Index()
        {
            return View(db.FollowTable.ToList());
        }
        // GET: FollowTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowTable followTable = db.FollowTable.Find(id);
            if (followTable == null)
            {
                return HttpNotFound();
            }
            return View(followTable);
        }

        // GET: FollowTables/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult My()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            Manage models = new Manage();
            models.FollowTable = user.followTable.ToList();
            models.FollowUser = user.followUser.ToList();
            return View(models);
        }
        // POST: FollowTables/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number")] FollowTable followTable)
        {
            if (ModelState.IsValid)
            {
                db.FollowTable.Add(followTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(followTable);
        }

        // GET: FollowTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowTable followTable = db.FollowTable.Find(id);
            if (followTable == null)
            {
                return HttpNotFound();
            }
            return View(followTable);
        }

        // POST: FollowTables/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number")] FollowTable followTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(followTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(followTable);
        }

        // GET: FollowTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowTable followTable = db.FollowTable.Find(id);
            if (followTable == null)
            {
                return HttpNotFound();
            }
            return View(followTable);
        }

        // POST: FollowTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FollowTable followTable = db.FollowTable.Find(id);
            db.FollowTable.Remove(followTable);
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
