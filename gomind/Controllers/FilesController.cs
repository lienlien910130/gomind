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
using System.Web.Hosting;

namespace gomind.Controllers
{
    public class FilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Files
        public ActionResult Index()
        {
            return View(db.File.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = db.File.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        public ActionResult GetImage(string id)
        {
            try
            {
                var file = db.UserFile.Where(f => f.User.Id == id).SingleOrDefault();
                if (file != null)
                {
                    byte[] buffer = file.Content;
                    return File(buffer, file.MimeType, file.Name);
                }
                else
                {
                    var files = db.File.Where(f => f.FileId == 6).SingleOrDefault();
                    byte[] buffers = files.Content;
                    return File(buffers, files.MimeType, files.Name);
                }
            }
            catch {
                throw new NotImplementedException();
            }
         
        }
        public ActionResult GetPImage(Guid id)
        {
           
                var file = db.File.Where(f => f.ProductList.number == id && f.number==1).SingleOrDefault();
                if (file != null) {
                byte[] buffer = file.Content;
                return File(buffer, file.MimeType, file.Name);
                }
                else
                {
                    var files = db.File.Where(f => f.number == 0).SingleOrDefault();
                    byte[] buffers = files.Content;
                    return File(buffers, files.MimeType, files.Name);
                }
        }
        public ActionResult GetImage2(Guid id)
        {
            try
            {
                var file = db.File.Where(f => f.ProductList.number == id && f.number == 2).SingleOrDefault();
                if (file != null)
                {
                    byte[] buffer = file.Content;
                    return File(buffer, file.MimeType, file.Name);
                }
                else
                {
                    var files = db.File.Where(f => f.number == 0).SingleOrDefault();
                    byte[] buffers = files.Content;
                    return File(buffers, files.MimeType, files.Name);
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        public ActionResult GetImage3(Guid id)
        {
            try
            {
                var file = db.File.Where(f => f.ProductList.number == id && f.number == 3).SingleOrDefault();
                if (file != null)
                {
                    byte[] buffer = file.Content;
                    return File(buffer, file.MimeType, file.Name);
                }
                else
                {
                    var files = db.File.Where(f => f.number == 0).SingleOrDefault();
                    byte[] buffers = files.Content;
                    return File(buffers, files.MimeType, files.Name);
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        public ActionResult GetImage4(Guid id)
        {
            try
            {
                var file = db.File.Where(f => f.ProductList.number == id && f.number == 4).SingleOrDefault();
                if (file != null)
                {
                    byte[] buffer = file.Content;
                    return File(buffer, file.MimeType, file.Name);
                }
                else {
                    var files = db.File.Where(f => f.number == 0).SingleOrDefault();
                    byte[] buffers = files.Content;
                    return File(buffers, files.MimeType, files.Name);
                }
         
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
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
            var newfile = new Models.File
            {
                Name = ImageName,
                MimeType = upload.ContentType,
                Size = upload.ContentLength,
                Content = buffer,
                number = 0
            };
            db.File.Add(newfile);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = db.File.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,ImageUrl,ImageUrl1,ImageUrl2,ImageUrl3")] Models.File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = db.File.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.File file = db.File.Find(id);
            db.File.Remove(file);
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
