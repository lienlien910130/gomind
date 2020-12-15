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
    public class Comment_MemberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment_Member
        public ActionResult Index()
        {
            return View(db.Comment_Member.ToList());
        }

        // GET: Comment_Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Member comment_Member = db.Comment_Member.Find(id);
            if (comment_Member == null)
            {
                return HttpNotFound();
            }
            return View(comment_Member);
        }

        // GET: Comment_Member/Create
        public ActionResult Create(string id , int oid)
        {
            Comment_Member m = new Comment_Member();
            m.Order = db.Order.Find(oid);
            m.touserid = id;
            return PartialView("_Comment",m);
        }

        public ActionResult Search(string id)
        {
            Session["userid"] = id;
            return View(db.Comment_Member.Where(f=>f.touserid==id).ToList());
        }
       

        // POST: Comment_Member/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment_Member comment_Member , string count)
        {
            var u = db.Users.Find(User.Identity.GetUserId());
            var o = db.Users.Find(comment_Member.touserid);
             var newcom = new Comment_Member
            {
                touserid = comment_Member.touserid,
                addtime = DateTime.Now,
                score =count,
                ccont = comment_Member.ccont,
                tousername = o.UserNickName,
                Order = db.Order.Find(comment_Member.Order.id),
                type = "1",
                edit=false
            };
            var or = db.Order.Find(comment_Member.Order.id);
            or.givco = true;
            var nullc = db.Comment_Member.FirstOrDefault(f => f.tousername == null);
            if (nullc != null) { db.Comment_Member.Remove(nullc); }
            u.comment_Member = new List<Comment_Member> { newcom };
            db.SaveChanges();
            TempData["message"] = "已新增評論給買家!";
            return RedirectToAction("Index","Manage"); 
        }

        public ActionResult Createto(string id, int oid)
        {
            Comment_Member m = new Comment_Member();
            m.Order = db.Order.Find(oid);
            m.touserid = db.Order.FirstOrDefault(f=>f.id==oid).User.Id;
            return PartialView("_Commentto", m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createto(Comment_Member comment_Member, string count)
        {
            var u = db.Users.Find(User.Identity.GetUserId());
            var o = db.Users.Find(comment_Member.touserid);
            var newcom = new Comment_Member
            {
                touserid = comment_Member.touserid,
                addtime = DateTime.Now,
                score = count,
                ccont = comment_Member.ccont,
                tousername = o.UserNickName,
                Order = db.Order.Find(comment_Member.Order.id),
                type = "1",
                edit=false
            };
            var or = db.Order.Find(comment_Member.Order.id);
            or.getco = true;
            var nullc = db.Comment_Member.FirstOrDefault(f => f.touserid == null);
            if (nullc != null) { db.Comment_Member.Remove(nullc); }
            u.comment_Member = new List<Comment_Member> { newcom };
            db.SaveChanges();
            TempData["message"] = "已新增評論給賣家!";
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Cancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return PartialView("_OrderList",user.order.ToList());
        }
        public ActionResult BuyCancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var cm = (from m in db.Comment_Member where m.touserid == user.Id && m.type == "1" select m).ToList();
            return PartialView("_BuyOrder", cm);
        }
        public ActionResult CommEditCancel()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return PartialView("_GiveComment", user.comment_Member.ToList());
        }
        // GET: Comment_Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Member comment_Member = db.Comment_Member.Find(id);
            if (comment_Member == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditComment",comment_Member);
        }

        // POST: Comment_Member/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment_Member comment_Member , int id, string count)
        {
            var u = db.Users.Find(User.Identity.GetUserId());
            var c = db.Comment_Member.Find(id);
            c.score = count;
            c.ccont = comment_Member.ccont;
            c.edit = true;
            db.SaveChanges();
            return PartialView("_GiveComment", u.comment_Member.ToList());
        }

        // GET: Comment_Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Member comment_Member = db.Comment_Member.Find(id);
            if (comment_Member == null)
            {
                return HttpNotFound();
            }
            return View(comment_Member);
        }

        // POST: Comment_Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment_Member comment_Member = db.Comment_Member.Find(id);
           
            db.Comment_Member.Remove(comment_Member);
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
