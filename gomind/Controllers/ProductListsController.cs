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
using System.IO;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PagedList.Mvc;
namespace gomind.Controllers
{
    public class ProductListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProductListsController()
        {
        }

        public ProductListsController(ApplicationUserManager userManager)
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
    
        // GET: ProductLists
        public ActionResult Index()
        {
            var productList = db.ProductList.Include(p => p.User);
            return View(productList.ToList());
        }

        // GET: ProductLists/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListDetails detail = new ProductListDetails();
            ProductList productList = db.ProductList.Find(id);
            detail.ProductList = productList;
            detail.Comment_Product = productList.Comment_Product.ToList();
            detail.Create_Comment = new Comment_Product();
            var us = db.Users.Find(productList.User.Id);
            detail.SaleSetting = us.saleSetting.ToList();
            detail.File = productList.File.ToList();
            if (User.Identity.IsAuthenticated)
            {
                detail.Follow = UserManager.SearchPFollow(User.Identity.GetUserId(), id);
                detail.isUser = UserManager.SearchUser(User.Identity.GetUserId(), id);
            }
            else
            {
                detail.Follow = false;
                detail.isUser = false;
            }
            if (productList == null)
            {
                return HttpNotFound();
            }
            var ss = db.Chat.Where(f => f.ProductList.number == id).ToList();
            foreach (var i in ss) {
                if (i.Order.Count() > 0)
                {
                    detail.IsOrder = true;
                    break;
                }
                else { detail.IsOrder = false; }
            }

            return View(detail);
        }

        // GET: ProductLists/Create
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.saleSetting.Count() == 0 || user.saleSetting == null)
            {
                var setting = new SaleSetting
                {
                    SendFace = false,
                    SendFamily = true,
                    SendHome = false,
                    SendPost = false,
                    SendSeven = true,
                    HomeMoney = null,
                    SevenMoney = 55,
                    FamilMoney = 55,
                    PostMoney = null
                };
                user.saleSetting = new List<SaleSetting> { setting };
                db.SaveChanges();
            }

            if (user.userData.Count() == 0 || user.userData == null)
            {
                var userdata = new UserData
                {
                    id = User.Identity.GetUserId(),
                    Nickname = UserManager.GetUsersname(User.Identity.GetUserId()),
                    Birthday = null,
                    number = 0,
                    content = "",
                    Sex = "",
                };
                user.userData = new List<UserData> { userdata };
                db.SaveChanges();
            }
            if (user.setting.Count() == 0 || user.setting == null)
            {
                var se = new Setting
                {
                    Talk = 0,
                    OrderNumber = 0,
                    TalkNumber = 0
                };
                user.setting = new List<Setting> { se };
                db.SaveChanges();
            }
            ProductListCreate model = new ProductListCreate();
            model.ProductList = new ProductList();
            model.SaleSetting = db.SaleSetting.FirstOrDefault(f =>f.User.Id == user.Id);
            List<SelectListItem> list = new List<SelectListItem>()
             {
                new SelectListItem() { Text = "全新", Value = "1"},
                new SelectListItem() { Text = "二手", Value = "2"}
             };
            ViewBag.Item = list;
            return View(model);
        }

        // POST: ProductLists/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductListCreate p, HttpPostedFileBase upload, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, int dropdownOne, int dropdownTwo, int status)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            One o1 = db.One.FirstOrDefault(f => f.OneId == dropdownOne);
            Two o2 = db.Two.FirstOrDefault(f => f.Id == dropdownTwo);
            ProductList pp = new ProductList();
            pp.number = Guid.NewGuid();
            pp.userid = user.Id;
            pp.prodlist_name = p.ProductList.prodlist_name;
            pp.prodlist_price = p.ProductList.prodlist_price;
            pp.count = p.ProductList.count;
            if (status == 1) { pp.prodlist_status = "全新"; }
            else { pp.prodlist_status = "二手"; }
            pp.plistsale_mnumber = UserManager.GetUserNickname(User.Identity.GetUserId());
            pp.prodlist_content = p.ProductList.prodlist_content;
            pp.prodlist_followtimes = 0;
            pp.createdate = DateTime.Now;
            pp.onekind = o1.OneKind;
            pp.twokind = o2.TwoKind;
            if (upload != null || upload1 != null || upload2 != null || upload3 != null)
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
                    number = 1
                };
                pp.File = new List<Models.File> { newfile };
                pp.url = System.IO.Path.GetFileName(upload.FileName);
                db.SaveChanges();
                if (upload1 != null)
                {
                    string ImageName1 = Path.GetFileName(upload1.FileName);
                    int length1 = upload1.ContentLength;
                    byte[] buffer1 = new byte[length1];
                    upload1.InputStream.Read(buffer1, 0, length1);
                    var newfile1 = new Models.File
                    {
                        Name = ImageName1,
                        MimeType = upload1.ContentType,
                        Size = upload1.ContentLength,
                        Content = buffer1,
                        number = 2
                    };
                    pp.File.Add(newfile1);
                    db.SaveChanges();
                }
                if (upload2 != null)
                {
                    string ImageName2 = Path.GetFileName(upload2.FileName);
                    int length2 = upload2.ContentLength;
                    byte[] buffer2 = new byte[length2];
                    upload2.InputStream.Read(buffer2, 0, length2);
                    var newfile2 = new Models.File
                    {
                        Name = ImageName2,
                        MimeType = upload2.ContentType,
                        Size = upload2.ContentLength,
                        Content = buffer2,
                        number = 3
                    };
                    pp.File.Add(newfile2);
                    db.SaveChanges();
                }
                if (upload3 != null)
                {
                    string ImageName3 = Path.GetFileName(upload3.FileName);
                    int length3 = upload3.ContentLength;
                    byte[] buffer3 = new byte[length3];
                    upload3.InputStream.Read(buffer3, 0, length3);
                    var newfile3 = new Models.File
                    {
                        Name = ImageName3,
                        MimeType = upload3.ContentType,
                        Size = upload3.ContentLength,
                        Content = buffer3,
                        number = 4
                    };
                    pp.File.Add(newfile3);
                    db.SaveChanges();
                }
                db.SaveChanges();
            }
            SaleSetting s = db.SaleSetting.FirstOrDefault(f => f.User.Id == user.Id);
         
            s.SendFace = p.SaleSetting.SendFace;
            s.SendHome = p.SaleSetting.SendHome;
            s.SendPost = p.SaleSetting.SendPost;
            s.SendSeven = p.SaleSetting.SendSeven;
            s.SendFamily = p.SaleSetting.SendFamily;
            s.HomeMoney = p.SaleSetting.HomeMoney;
            s.PostMoney = p.SaleSetting.PostMoney;
            s.SevenMoney = p.SaleSetting.SevenMoney;
            s.FamilMoney = p.SaleSetting.FamilMoney;
            user.productList = new List<ProductList> { pp };
            db.ProductList.Add(pp);
            db.SaveChanges();
            TempData["message"] = "商品已成功上架!";
            return RedirectToAction("Saler", "UserDatas");
        }
       
      
        public ActionResult Search(string search, string dropdownlist)
        {
            var p = from s in db.ProductList select s;
       
            if (!String.IsNullOrEmpty(search))
            {
                p = p.Where(s => s.prodlist_name.Contains(search));
            }
            if (dropdownlist != null && dropdownlist != "") {
            switch (dropdownlist)
            {
                case "1":
                    p = p.OrderByDescending(s => s.prodlist_price);
                    break;
                case "2":
                    p = p.OrderBy(s => s.prodlist_price);
                    break;
                case "3":
                    p = p.OrderByDescending(s => s.createdate);
                    break;
                case "4":
                    p = p.OrderBy(s => s.createdate);
                    break;
                case "5":
                    p = p.Where(f=>f.prodlist_status=="全新");
                    break;
                default:
                    p = p.Where(f => f.prodlist_status == "二手");
                    break;
            }
            }
            return PartialView("_Product", p);
        }

        public ActionResult SearchSelf(string search , string id , string dropdownlist)
        {
            var p = from s in db.ProductList select s;

            if (!String.IsNullOrEmpty(search))
            {
                p = p.Where(s => s.prodlist_name.Contains(search));
                switch (dropdownlist)
                {
                    case "1":
                        p = p.OrderByDescending(s => s.prodlist_price);
                        break;
                    case "2":
                        p = p.OrderBy(s => s.prodlist_price);
                        break;
                    case "3":
                        p = p.OrderByDescending(s => s.createdate);
                        break;
                    case "4":
                        p = p.OrderBy(s => s.createdate);
                        break;
                    case "5":
                        p = p.Where(f => f.prodlist_status == "全新");
                        break;
                    default:
                        p = p.Where(f => f.prodlist_status == "二手");
                        break;
                }
                return PartialView("_Product", p);
            }
            else
            {
                var w = from s in db.ProductList where s.User.Id == id select s;
            switch (dropdownlist)
            {
                case "1":
                    w = w.OrderByDescending(s => s.prodlist_price);
                    break;
                case "2":
                    w = w.OrderBy(s => s.prodlist_price);
                    break;
                case "3":
                    w = w.OrderByDescending(s => s.createdate);
                    break;
                case "4":
                    w = w.OrderBy(s => s.createdate);
                    break;
                case "5":
                    w = w.Where(f => f.prodlist_status == "全新");
                    break;
                default:
                    w = w.Where(f => f.prodlist_status == "二手");
                    break;
            }
                 return PartialView("_Product", w);
          }
        }
        public ActionResult SearchKindSelf(int id, string uid)
        {

            One o1 = db.One.FirstOrDefault(f => f.OneId == id);
            var p = from s in db.ProductList where s.User.Id == uid && s.onekind == o1.OneKind select s;
            return PartialView("_Product", p);
        }
        private int pageSize = 9;

        public ActionResult Detailkindo(string id , int page = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>()
             {
                new SelectListItem() { Text = "價錢(高到低)", Value = "1"},
                new SelectListItem() { Text = "價錢(低到高)", Value = "2"},
                new SelectListItem() { Text = "商品(新到舊)", Value = "3"},
                new SelectListItem() { Text = "商品(舊到新)", Value = "4"},
                new SelectListItem() { Text = "種類(全新)", Value = "5"},
                new SelectListItem() { Text = "種類(二手)", Value = "6"},
             };
            ViewBag.Item = list;
            Session["userid"] = User.Identity.GetUserId();
            int currentPage = page < 1 ? 1 : page;
            var p = db.ProductList.Where(f => f.onekind == id).OrderByDescending(f => f.createdate);
            var result = p.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        public ActionResult Detailkindt(string id, int page = 1)
        {
            List<SelectListItem> list = new List<SelectListItem>()
             {
                new SelectListItem() { Text = "價錢(高到低)", Value = "1"},
                new SelectListItem() { Text = "價錢(低到高)", Value = "2"},
                new SelectListItem() { Text = "商品(新到舊)", Value = "3"},
                new SelectListItem() { Text = "商品(舊到新)", Value = "4"},
                new SelectListItem() { Text = "種類(全新)", Value = "5"},
                new SelectListItem() { Text = "種類(二手)", Value = "6"},
             };
            ViewBag.Item = list;
            Session["userid"] = User.Identity.GetUserId();
            int currentPage = page < 1 ? 1 : page;
            var p = db.ProductList.Where(f => f.twokind == id).OrderByDescending(f => f.createdate);
            var result = p.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        public ActionResult SearchKind(int id)
        {
            Two o2 = db.Two.FirstOrDefault(f => f.Id == id);
            var p = from s in db.ProductList where s.twokind == o2.TwoKind select s;
            return PartialView("_Product", p);
        }
        public ActionResult SearchKindname(string name)
        {
            One o1 = db.One.FirstOrDefault(f => f.OneKind == name);
            var p = from s in db.ProductList where s.onekind == o1.OneKind select s;
            return PartialView("_Product", p);
        }
        public ActionResult SearchOneKind(int id)
        {
            One o1 = db.One.FirstOrDefault(f => f.OneId == id);
            var p = from s in db.ProductList where s.onekind == o1.OneKind select s;
            return PartialView("_Product", p);
        }
        [Authorize]
        public ActionResult BeforeLogin(Guid id)
        {

            var user = db.Users.Find(User.Identity.GetUserId());
            ProductList pro = db.ProductList.Find(id);
            if (pro.userid == User.Identity.GetUserId())
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                if (user.followTable.FirstOrDefault(f => f.ProductList.number == id) != null)
                {
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    var follow = new FollowTable
                    {
                    
                        ProductList = db.ProductList.Find(id)
                    };
                    user.followTable = new List<FollowTable> { follow };
                    ProductList p = db.ProductList.Find(id);
                    p.prodlist_followtimes++;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = id });
                }
            }

        }
        [Authorize]
        public ActionResult AddProductFollow(Guid id)
        {

            var user = db.Users.Find(User.Identity.GetUserId());
            ProductList pro = db.ProductList.Find(id);
            if (pro.userid == User.Identity.GetUserId())
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                if (user.followTable.FirstOrDefault(f => f.ProductList.number == id) != null)
                {
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    var follow = new FollowTable
                    {
                        ProductList = db.ProductList.Find(id)
                    };
                    user.followTable = new List<FollowTable> { follow };
                    ProductList p = db.ProductList.Find(id);
                    p.prodlist_followtimes++;
                    db.SaveChanges();
                    TempData["message"] = "商品已加入我的最愛!";
                    return RedirectToAction("Details", new { id = id });
                }

            }

        }        
        [Authorize]
        public ActionResult RemoveProductFollow(Guid id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var follow = user.followTable.FirstOrDefault(f => f.ProductList.number == id);
            if (follow !=null) { db.FollowTable.Remove(follow); }
            ProductList p = db.ProductList.Find(id);
            p.prodlist_followtimes--;
            db.SaveChanges();
       
            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var has = db.ProductList.Find(id);
            var user = db.Users.FirstOrDefault(f => f.Id == has.User.Id);
            ProductListEdits model = new ProductListEdits();
            model.ProductList = db.ProductList.Find(id);
            model.SaleSetting = db.SaleSetting.FirstOrDefault(f => f.User.Id == has.User.Id);
            model.File = db.File.FirstOrDefault(f=>f.ProductList.number == id);
            model.o = db.One.FirstOrDefault(f=>f.OneKind==has.onekind).OneId;
            model.t = db.Two.FirstOrDefault(f => f.TwoKind == has.twokind).Id;
            List<SelectListItem> list = new List<SelectListItem>()
             {
                new SelectListItem() { Text = "全新", Value = "1"},
                new SelectListItem() { Text = "二手", Value = "2"}
             };
            ViewBag.Item = list;
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: ProductLists/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductListEdits model, HttpPostedFileBase upload, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3, int dropdownOne, int dropdownTwo, int status)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            ProductList p = db.ProductList.Find(model.ProductList.number);
            p.prodlist_name = model.ProductList.prodlist_name;
            p.prodlist_content = model.ProductList.prodlist_content;
            p.prodlist_price = model.ProductList.prodlist_price;
            p.count = model.ProductList.count;
           if (status == 1) { p.prodlist_status = "全新"; }
            else { p.prodlist_status = "二手"; }
            One o1 = db.One.FirstOrDefault(f => f.OneId == dropdownOne);
            Two o2 = db.Two.FirstOrDefault(f => f.Id == dropdownTwo);
            p.onekind = o1.OneKind;
            p.twokind = o2.TwoKind;
            if (upload != null || upload1 != null || upload2 != null || upload3 != null)
            {
                if (upload != null)
                {
                    string ImageName = Path.GetFileName(upload.FileName);
                    int length = upload.ContentLength;
                    byte[] buffer = new byte[length];
                    upload.InputStream.Read(buffer, 0, length);
                    var p1 = db.File.FirstOrDefault(f => f.ProductList.number == p.number && f.number == 1);
                    if (p1 != null)
                    {
                        p1.Name = ImageName;
                        p1.MimeType = upload.ContentType;
                        p1.Size = upload.ContentLength;
                        p1.Content = buffer;
                    }
                    else
                    {
                        var newfile = new Models.File
                        {
                            Name = ImageName,
                            MimeType = upload.ContentType,
                            Size = upload.ContentLength,
                            Content = buffer,
                            number = 1
                        };
                        p.File.Add(newfile);
                    }
                    p.url = System.IO.Path.GetFileName(upload.FileName);
                    db.SaveChanges();
                }
                    if (upload1 != null)
                    {
                        string ImageName1 = Path.GetFileName(upload1.FileName);
                        int length1 = upload1.ContentLength;
                        byte[] buffer1 = new byte[length1];
                        upload1.InputStream.Read(buffer1, 0, length1);
                        var p2 = db.File.FirstOrDefault(f => f.ProductList.number == p.number && f.number == 2);
                        if (p2 != null)
                        {
                            p2.Name = ImageName1;
                            p2.MimeType = upload1.ContentType;
                            p2.Size = upload1.ContentLength;
                            p2.Content = buffer1;
                        }
                        else
                        {
                            var newfile = new Models.File
                            {
                                Name = ImageName1,
                                MimeType = upload1.ContentType,
                                Size = upload1.ContentLength,
                                Content = buffer1,
                                number = 2
                            };
                            p.File.Add(newfile);
                        }
                        db.SaveChanges();
                    }
                    if (upload2 != null)
                    {
                        string ImageName2 = Path.GetFileName(upload2.FileName);
                        int length2 = upload2.ContentLength;
                        byte[] buffer2 = new byte[length2];
                        upload2.InputStream.Read(buffer2, 0, length2);
                        var p3 = db.File.FirstOrDefault(f => f.ProductList.number == p.number && f.number == 3);
                        if (p3 != null)
                        {
                            p3.Name = ImageName2;
                            p3.MimeType = upload2.ContentType;
                            p3.Size = upload2.ContentLength;
                            p3.Content = buffer2;
                        }
                        else
                        {
                            var newfile = new Models.File
                            {
                                Name = ImageName2,
                                MimeType = upload2.ContentType,
                                Size = upload2.ContentLength,
                                Content = buffer2,
                                number = 3
                            };
                            p.File.Add(newfile);
                        }
                        db.SaveChanges();
                    }
                    if (upload3 != null)
                    {
                        string ImageName3 = Path.GetFileName(upload3.FileName);
                        int length3 = upload3.ContentLength;
                        byte[] buffer3 = new byte[length3];
                        upload3.InputStream.Read(buffer3, 0, length3);
                        var p4 = db.File.FirstOrDefault(f => f.ProductList.number == p.number && f.number == 3);
                        if (p4 != null)
                        {
                            p4.Name = ImageName3;
                            p4.MimeType = upload3.ContentType;
                            p4.Size = upload3.ContentLength;
                            p4.Content = buffer3;
                        }
                        else
                        {
                            var newfile = new Models.File
                            {
                                Name = ImageName3,
                                MimeType = upload3.ContentType,
                                Size = upload3.ContentLength,
                                Content = buffer3,
                                number = 4
                            };
                            p.File.Add(newfile);
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
            
          }
           
            SaleSetting s = db.SaleSetting.FirstOrDefault(f => f.User.Id == user.Id);
        
            s.SendFace = model.SaleSetting.SendFace;
            s.SendHome = model.SaleSetting.SendHome;
            s.SendPost = model.SaleSetting.SendPost;
            s.SendSeven = model.SaleSetting.SendSeven;
            s.SendFamily = model.SaleSetting.SendFamily;
            s.HomeMoney = model.SaleSetting.HomeMoney;
            s.PostMoney = model.SaleSetting.PostMoney;
            s.SevenMoney = model.SaleSetting.SevenMoney;
            s.FamilMoney = model.SaleSetting.FamilMoney;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = p.number });
        }

        // GET: ProductLists/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductList.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductList productList = db.ProductList.Find(id);
            if (productList.File!=null) {
                var fi = db.File.Where(f => f.ProductList.number == id);
                foreach (var i in fi) { db.File.Remove(i); }
            }
            if (productList.Comment_Product!=null) {
                var cop = db.Comment_Product.Where(f => f.ProductList.number == id);
                foreach (var i in cop) { db.Comment_Product.Remove(i); }
            }
            if (productList.FollowTable!=null) {
                var ft = db.FollowTable.Where(f => f.ProductList.number == id);
                foreach (var i in ft) { db.FollowTable.Remove(i); }
            }
            if (productList.Chat!=null) {
               var ch = db.Chat.Where(f => f.ProductList.number == id).ToList();
                foreach (var i in ch)
                {
                    if (i.ChatMessage.Count()>0)
                    {
                        var me = db.ChatMessage.Where(f => f.Chat.ID == i.ID);
                        foreach (var q in me) { db.ChatMessage.Remove(q); }
                        if (i.Order.Count()>0)
                        {
                            var or = db.Order.Where(f => f.Chat.ID == i.ID).ToList();
                            foreach (var q in or) {
                                if (q.Comment_Member.Count() >0) {
                                    var cme = db.Comment_Member.Where(f => f.Order.id == q.id);
                                    foreach (var qs in cme) { db.Comment_Member.Remove(qs); }
                                }
                                db.Order.Remove(q); }
                        }
                    }
                    db.Chat.Remove(i);
                }
            }
            var a = db.Users.Find(User.Identity.GetUserId());
            db.ProductList.Remove(productList);
            db.SaveChanges();
            TempData["message"] = "商品已刪除!";
            return RedirectToAction("Salers","UserDatas",new {id=a.Id });
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
