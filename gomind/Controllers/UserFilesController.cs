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
using System.IO;

namespace gomind.Controllers
{
    public class UserFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserFiles
        public ActionResult Index()
        {
            return View(db.UserFile.ToList());
        }

        // GET: UserFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile userFile = db.UserFile.Find(id);
            if (userFile == null)
            {
                return HttpNotFound();
            }
            return View(userFile);
        }

        // GET: UserFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserFiles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase upload)
        {
            string ImageName = Path.GetFileName(upload.FileName);
            int length = upload.ContentLength;
            byte[] buffer = new byte[length];
            upload.InputStream.Read(buffer, 0, length);
            var newfile = new UserFile
            {
                Name = ImageName,
                MimeType = upload.ContentType,
                Size = upload.ContentLength,
                Content = buffer
            };
  
            db.UserFile.Add(newfile);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
           
        }

        // GET: UserFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile userFile = db.UserFile.Find(id);
            if (userFile == null)
            {
                return HttpNotFound();
            }
            return View(userFile);
        }

        // POST: UserFiles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,ImageUrl")] UserFile userFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userFile);
        }

        // GET: UserFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFile userFile = db.UserFile.Find(id);
            if (userFile == null)
            {
                return HttpNotFound();
            }
            return View(userFile);
        }

        // POST: UserFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserFile userFile = db.UserFile.Find(id);
            db.UserFile.Remove(userFile);
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
