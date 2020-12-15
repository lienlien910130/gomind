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
using Microsoft.AspNet.Identity.Owin;

namespace gomind.Controllers
{
    public class Comment_ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Comment_ProductController()
        {
        }

        public Comment_ProductController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Comment_Product
        public ActionResult Index()
        {
            return View(db.Comment_Product.ToList());
        }

        // GET: Comment_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Product comment_Product = db.Comment_Product.Find(id);
            if (comment_Product == null)
            {
                return HttpNotFound();
            }
            return View(comment_Product);
        }

        // GET: Comment_Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment_Product/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductListDetails details, Guid? id)
        {
            if (ModelState.IsValid)
            {
                ProductList productList = db.ProductList.Find(id);
                var us = db.Users.Find(User.Identity.GetUserId());
                var com = new Comment_Product
                {
                    prccont = details.Create_Comment.prccont,
                    prctime = DateTime.Now,
                    User = us
                };
                productList.Comment_Product = new List<Comment_Product> { com };
                db.SaveChanges();
                TempData["message"] = "新增評論成功!";
                return RedirectToAction("Details","ProductLists", new { id = productList.number });
            }

            return View(details);
        }

     
        // GET: Comment_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Product comment_Product = db.Comment_Product.Find(id);
            if (comment_Product == null)
            {
                return HttpNotFound();
            }
            return View(comment_Product);
        }

        // POST: Comment_Product/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number,snumber,bnumberid,bnumbername,prccont,prcscore,prctime")] Comment_Product comment_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment_Product);
        }

        // GET: Comment_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment_Product comment_Product = db.Comment_Product.Find(id);
            if (comment_Product == null)
            {
                return HttpNotFound();
            }
            return View(comment_Product);
        }

        // POST: Comment_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment_Product comment_Product = db.Comment_Product.Find(id);
            db.Comment_Product.Remove(comment_Product);
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
