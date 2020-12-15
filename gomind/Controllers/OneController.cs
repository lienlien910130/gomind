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
    public class OneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: One
        public ActionResult Index()
        {
            return View(db.One.ToList());
        }

        // GET: One/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            One one = db.One.Find(id);
            if (one == null)
            {
                return HttpNotFound();
            }
            return View(one);
        }

        // GET: One/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: One/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OneId,OneKind")] One one)
        {
            if (ModelState.IsValid)
            {
                db.One.Add(one);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(one);
        }

        // GET: One/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            One one = db.One.Find(id);
            if (one == null)
            {
                return HttpNotFound();
            }
            return View(one);
        }

        // POST: One/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OneId,OneKind")] One one)
        {
            if (ModelState.IsValid)
            {
                db.Entry(one).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(one);
        }

        // GET: One/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            One one = db.One.Find(id);
            if (one == null)
            {
                return HttpNotFound();
            }
            return View(one);
        }

        // POST: One/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            One one = db.One.Find(id);
            db.One.Remove(one);
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
