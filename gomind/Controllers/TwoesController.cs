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
    public class TwoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Twoes
        public ActionResult Index()
        {
            return View(db.Two.ToList());
        }

        // GET: Twoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Two two = db.Two.Find(id);
            if (two == null)
            {
                return HttpNotFound();
            }
            return View(two);
        }

        // GET: Twoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Twoes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OneId,TwoKind")] Two two)
        {
            if (ModelState.IsValid)
            {
                db.Two.Add(two);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(two);
        }

        // GET: Twoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Two two = db.Two.Find(id);
            if (two == null)
            {
                return HttpNotFound();
            }
            return View(two);
        }

        // POST: Twoes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OneId,TwoKind")] Two two)
        {
            if (ModelState.IsValid)
            {
                db.Entry(two).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(two);
        }

        // GET: Twoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Two two = db.Two.Find(id);
            if (two == null)
            {
                return HttpNotFound();
            }
            return View(two);
        }

        // POST: Twoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Two two = db.Two.Find(id);
            db.Two.Remove(two);
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
