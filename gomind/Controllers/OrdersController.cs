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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var u = db.Users.Find(User.Identity.GetUserId());
            var o = db.Order.FirstOrDefault(f => f.Chat.ID == id);
            var c = db.Chat.FirstOrDefault(f => f.ID == id);
            if (o != null)
            {
                TempData["message"] = "訂單已成立,請至訂單管理查詢!";
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                var order = new Order
                {
                    buyerid = c.User.Id,
                    name = c.User.UserNickName,
                    createtime = DateTime.Now,
                    send = false,
                    pay = false,
                    Chat = db.Chat.Find(id),
                    getco = false,
                    givco = false
                };
                u.order = new List<Order> { order };
                var nullc = db.Comment_Member.FirstOrDefault(f => f.tousername == null);
                if (nullc != null) {
                    db.Comment_Member.Remove(nullc);
                }
                db.SaveChanges();
                TempData["message"] = "訂單已成立,請至訂單管理查詢!";
                return RedirectToAction("Index", "Manage");
            }

        }

        // POST: Orders/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(int id)
        //{
        //    var u = db.Users.Find(User.Identity.GetUserId());
        //    var o = db.Order.FirstOrDefault(f =>f.Chat.ID==id);
        //    var c = db.Chat.FirstOrDefault(f=>f.ID==id);
        //    if (o != null)
        //    {
        //        TempData["message"] = "訂單已成立,請至購買紀錄查詢!";
        //        return RedirectToAction("Index", "Manage");
        //    }
        //    else
        //    {
        //        var order = new Order
        //        {
        //            buyerid =c.User.Id,
        //            createtime = DateTime.Now,
        //            send = false,
        //            pay = false,
        //            Chat = db.Chat.Find(id)
        //        };
        //        u.order = new List<Order> { order };
        //        db.Order.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Manage");
        //    }

        //}

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return PartialView("_OrderEdit",order);
        }
        public ActionResult Cancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            return PartialView("_OrderList", user.order.ToList());
        }
        // POST: Orders/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order , int id)
        {

            Order o = db.Order.Find(id);
            o.pay = order.pay;
            o.send = order.send;
            db.SaveChanges();
            return RedirectToAction("Cancel");
          
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return PartialView("_OrderDelete",order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.Order.Find(id);
            if (order.Comment_Member.Count>0 || order.Comment_Member!=null) {
                var co = db.Comment_Member.Where(f=>f.Order.id==id).ToList();
                foreach (var x in co) {db.Comment_Member.Remove(x);}
            }
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Cancel");
        }

        public ActionResult De(int id) {
            var order = db.Order.Find(id);
            if (order.Comment_Member.Count > 0 || order.Comment_Member != null)
            {
                var co = db.Comment_Member.Where(f => f.Order.id == id).ToList();
                foreach (var x in co) { db.Comment_Member.Remove(x); }
            }
            db.Order.Remove(order);
            db.SaveChanges();
            TempData["message"] = "訂單已刪除!";
            return RedirectToAction("Index","Manage");
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
