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
    public class SaleSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SaleSettings
        public ActionResult Index()
        {
            return View(db.SaleSetting.ToList());
        }

        // GET: SaleSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleSetting saleSetting = db.SaleSetting.Find(id);
            if (saleSetting == null)
            {
                return HttpNotFound();
            }
            return View(saleSetting);
        }

        // GET: SaleSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleSettings/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,SendFace,SendATM,SendHome,SendSeven,SendFamily,SendPost,HomeMoney,SevenMoney,FamilMoney,PostMoney")] SaleSetting saleSetting)
        {
            if (ModelState.IsValid)
            {
                db.SaleSetting.Add(saleSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleSetting);
        }

        // GET: SaleSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleSetting saleSetting = db.SaleSetting.Find(id);
            if (saleSetting == null)
            {
                return HttpNotFound();
            }
            return PartialView("_SaleSettingEdit", saleSetting);
        }

        // POST: SaleSettings/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,SendFace,SendATM,SendHome,SendSeven,SendFamily,SendPost,HomeMoney,SevenMoney,FamilMoney,PostMoney")] SaleSetting saleSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleSetting).State = EntityState.Modified;
                db.SaveChanges();
                var user = db.Users.Find(User.Identity.GetUserId());
                return PartialView("_SaleSettingIndex", user.saleSetting.ToList());
            }
            return View(saleSetting);
        }
        [HttpPost]
        public ActionResult EditCancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return PartialView("_SaleSettingIndex", user.saleSetting.ToList());

        }
        // GET: SaleSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleSetting saleSetting = db.SaleSetting.Find(id);
            if (saleSetting == null)
            {
                return HttpNotFound();
            }
            return View(saleSetting);
        }

        // POST: SaleSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleSetting saleSetting = db.SaleSetting.Find(id);
            db.SaleSetting.Remove(saleSetting);
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
