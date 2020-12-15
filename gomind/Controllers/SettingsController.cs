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
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Settings
        public ActionResult Index()
        {
            return View(db.Setting.ToList());
        }

        // GET: Settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Setting.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,NewComment,NewFollower,NewProComment,NewTalk,NewPFollow")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                db.Setting.Add(setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setting);
        }

        // GET: Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Setting.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return PartialView("_SettingEdit", setting);
        }
        public JsonResult Count(string id) {

            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.setting.Count() == 0 || user.setting == null)
            {
                var se = new Setting
                {
                  
                    OrderNumber = 0,
                    TalkNumber = 0
                };
                user.setting = new List<Setting> { se };
                db.SaveChanges();
            }
            var order = db.Order.Where(f => f.buyerid == user.Id).OrderBy(x=>x.createtime);
            var talk = db.Chat.Where(f => f.toUserId == user.Id && f.ChatMessage.Count() > 0);
            var setting = db.Setting.FirstOrDefault(f=>f.User.Id==user.Id);
            if (order.Count() != setting.OrderNumber) {
                var or = order.Skip(setting.OrderNumber);
                var result = new LinkedList<object>();
               foreach (var mes in or)
               {
                    result.AddLast(new
                    {
                        Username = mes.User.UserNickName,
                        PostDateTime = mes.createtime.ToString(),
                        MessageBody ="此賣家已成立訂單,可至會員管理查看!"
                    });
               }
               return Json(result, JsonRequestBehavior.AllowGet);
            }
            var results = new { message = "Success" };
            return Json(results, JsonRequestBehavior.AllowGet);
           
        }
        // POST: Settings/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Setting setting)
        //{
        //    var user = db.Users.Find(User.Identity.GetUserId());
        //    var u = db.Setting.FirstOrDefault(f => f.User.Id == user.Id);
        //    if (setting.NewOrder != u.NewOrder)
        //    {
        //        if (setting.NewOrder == true)
        //        {
        //            var order = db.Order.Where(f => f.buyerid == user.Id);
        //            u.OrderNumber = order.Count();
        //        }
        //    }
        //    if (setting.NewTalk != u.NewTalk)
        //    {
        //        if (setting.NewTalk == true)
        //        {
        //            var talk = db.Chat.Where(f => f.toUserId == user.Id && f.ChatMessage.Count() > 0);
        //            u.TalkNumber = talk.Count();
        //        }
        //    }
        //    u.NewOrder = setting.NewOrder;
        //    u.NewTalk = setting.NewTalk;
        //    db.SaveChanges();
        //    return PartialView("_SettingIndex", user.setting.ToList());
        //}
        public ActionResult Cancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return PartialView("_SettingIndex", user.setting.ToList());
        }
        // GET: Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = db.Setting.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Setting setting = db.Setting.Find(id);
            db.Setting.Remove(setting);
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
