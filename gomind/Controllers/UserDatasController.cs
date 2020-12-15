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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using System.Web.WebPages;

namespace gomind.Controllers
{
    public class UserDatasController : Controller
    {
  
        private ApplicationDbContext db = new ApplicationDbContext();
        public UserDatasController()
        {
        }

        public UserDatasController(ApplicationUserManager userManager)
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
        // GET: UserDatas
        public ActionResult Index()
        {
            return View(db.UserData.ToList());
        }

        // GET: UserDatas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.UserData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }
        public ActionResult Saler(sale sale)
        {
            var us = db.Users.Find(User.Identity.GetUserId());
            UserData user = db.UserData.Find(User.Identity.GetUserId());
            if (us.userData == null || us.userData.Count() == 0)
            {
                var userdata = new UserData
                {
                    id = User.Identity.GetUserId(),
                    Nickname = UserManager.GetUsersname(User.Identity.GetUserId()),
                    Birthday = null,
                 
                    Sex = "",
                    number = 0,
                    content = ""
                };
                us.userData = new List<UserData> { userdata };
                db.SaveChanges();
            }
            if (us.saleSetting.Count() == 0 || us.saleSetting == null)
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
                us.saleSetting = new List<SaleSetting> { setting };
                db.SaveChanges();
            }
            if (us.setting.Count() == 0 || us.setting == null)
            {
                var se = new Setting
                {
                   Talk=0,
                    OrderNumber = 0,
                    TalkNumber = 0
                };
                us.setting = new List<Setting> { se };
                db.SaveChanges();
            }
            var a = new List<One>();
            var fi = us.productList.Select(m => m.onekind);
            foreach (var i in fi)
            {
                var it = db.One.FirstOrDefault(f => f.OneKind == i);
                if (a.Contains(it) != true)
                {
                    a.Add(it);
                }
                else { continue; }
            }
            var b = new List<One>();
            foreach (var i in fi)
            {
                var it = db.One.FirstOrDefault(f => f.OneKind == i);
                 b.Add(it);
               
            }
            var kk = new kind {
                id = us.Id,
                one = a ,
                oneall = b
            };
            sale.kind = new List<kind>{ kk };
            sale.IsUser = true;
            sale.ProductList = us.productList.ToList();
            sale.UserData = us.userData.ToList();
            sale.UserFile = us.userFile.ToList();
            sale.Follow = false;
            sale.id = User.Identity.GetUserId();
            sale.img = UserManager.GetImg(User.Identity.GetUserId());
            var nus = 0;
            var number = db.Comment_Member.Where(f => f.touserid == us.Id).Select(x => x.score);
            var oness = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "1").Count();
            var twoss = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "2").Count();
            var threess = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "3").Count();
            var fourss = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "4").Count();
            var fivess = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "5").Count();
            foreach (var i in number) {  nus = nus +  i.AsInt(); }
            var co = number.Count();
            sale.count = co;
            sale.foll = db.UserData.FirstOrDefault(f => f.id == us.Id).number;
            sale.star = ((float)nus / (float)co).ToString();
            sale.ones = ((float)oness / (float)co * 100).ToString();
            sale.twos = ((float)twoss / (float)co * 100).ToString();
            sale.three = ((float)threess / (float)co * 100).ToString();
            sale.four = ((float)fourss / (float)co * 100).ToString();
            sale.five = ((float)fivess / (float)co *100).ToString();
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
            return View(sale);
        }
        public ActionResult Salers(string id)
        {
            var us = db.Users.Find(id);
            if (us.userData == null || us.userData.Count() == 0)
            {
                var userdata = new UserData
                {
                    id = id,
                    Nickname = UserManager.GetUsersname(id),
                    Birthday = null,
                    Sex = "",
                    number = 0,
                    content = ""
                };
                us.userData = new List<UserData> { userdata };
                db.SaveChanges();
            }
            if (us.saleSetting.Count() == 0 || us.saleSetting == null)
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
                us.saleSetting = new List<SaleSetting> { setting };
                db.SaveChanges();
            }
            if (us.setting.Count() == 0 || us.setting == null)
            {
                var se = new Setting
                {
                    Talk=0,
                    OrderNumber = 0,
                    TalkNumber = 0
                };
                us.setting = new List<Setting> { se };
                db.SaveChanges();
            }
            sale sa = new sale();
            if (id == User.Identity.GetUserId())
            {
                sa.IsUser = true;
            }
            else { sa.IsUser = false; }
            var a = new List<One>();
            var fi = us.productList.Select(m => m.onekind);
            foreach (var i in fi)
            {
                var it = db.One.FirstOrDefault(f => f.OneKind == i);
                if (a.Contains(it) != true)
                {
                    a.Add(it);
                }
                else { continue; }
            }
            var b = new List<One>();
            foreach (var i in fi)
            {
                var it = db.One.FirstOrDefault(f => f.OneKind == i);
                b.Add(it);

            }
            var kk = new kind
            {
                id = us.Id,
                one = a,
                oneall = b
            };
            sa.kind = new List<kind> { kk };
            sa.UserFile = us.userFile.ToList();
            sa.UserData = us.userData.ToList();
            sa.ProductList = us.productList.ToList();
            sa.Comment_Member = us.comment_Member.ToList();
            if (User.Identity.IsAuthenticated)
            {
                sa.Follow = UserManager.SearchUSollow(User.Identity.GetUserId(), id);
            }
            else { sa.Follow = false; }
            sa.id = id;
            sa.img = UserManager.GetImg(id);
            var nus = 0;
            var number = db.Comment_Member.Where(f => f.touserid == us.Id).Select(x => x.score);
            var oness = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "1").Count();
            var twoss = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "2").Count();
            var threess = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "3").Count();
            var fourss = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "4").Count();
            var fivess = db.Comment_Member.Where(f => f.touserid == us.Id && f.score == "5").Count();
            foreach (var i in number)
            {
                nus = nus + i.AsInt();
            }
            var co = number.Count();
            sa.count = co;
            sa.foll = db.UserData.FirstOrDefault(f => f.id == us.Id).number;
            sa.star = ((float)nus / (float)co).ToString();
            sa.ones = ((float)oness / (float)co * 100).ToString();
            sa.twos = ((float)twoss / (float)co * 100).ToString();
            sa.three = ((float)threess / (float)co * 100).ToString();
            sa.four = ((float)fourss / (float)co * 100).ToString();
            sa.five = ((float)fivess / (float)co * 100).ToString();
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
            return View(sa);
        }

        [HttpGet]
        public ActionResult SalerIndex()
        {
            var us = db.Users.Find(User.Identity.GetUserId());
            sale sa = new sale();
            sa.IsUser = true;
            sa.UserData = us.userData.ToList();
            return PartialView("_SalerContentIndex", sa);
        }

        [HttpGet]
        public ActionResult SalerEdit(sale sale)
        {
            return PartialView("_SalerContentEdit", sale.UserData);
        }

        [HttpPost]
        public ActionResult SalerEdit(string message)
        {
            UserData us = db.UserData.Find(User.Identity.GetUserId());
            us.content = message;
            db.SaveChanges();
            return RedirectToAction("SalerIndex");
        }

        [Authorize]
        public ActionResult BeforeFollow(string id)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            ApplicationUser fuser = db.Users.Find(id);
            if (fuser.Id == User.Identity.GetUserId())
            {
                return RedirectToAction("Salers", new { id = id });
            }
            else
            {
                if (user.followUser.FirstOrDefault(f => f.userid == id) != null)
                {
                    return RedirectToAction("Salers", new { id = id });
                }
                else
                {
                    var follow = new FollowUser
                    {
                        followuserid = fuser.Id,
                        username=fuser.UserNickName
                    };
                    user.followUser = new List<FollowUser> { follow };
                    UserData us = db.UserData.Find(id);
                    us.number++;
                    db.SaveChanges();
                    return RedirectToAction("Salers", new { id = id });
                }
            }

        }
       
        [Authorize]
        public ActionResult AddUserFollow(string id)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            ApplicationUser fuser = db.Users.Find(id);
            UserFile uf = db.UserFile.FirstOrDefault(f=>f.User.Id==fuser.Id);
            if (fuser.Id == User.Identity.GetUserId())
            {
             
                return RedirectToAction("Salers", new { id = id });
            }
            else
            {
                if (user.followUser.FirstOrDefault(f => f.User.Id == id) != null)
                {
                    return RedirectToAction("Salers", new { id = id });
                }
                else
                {
                    var follow = new FollowUser
                    {
                        followuserid = fuser.Id,
                        username = fuser.UserNickName
                    };
                    user.followUser = new List<FollowUser> { follow };
                    UserData us = db.UserData.Find(id);
                    us.number++;
                    db.SaveChanges();
                    TempData["message"] = "已成功追蹤此賣家!";
                    return RedirectToAction("Salers", new { id = id });
                }
            }


        }
        [Authorize]
        public ActionResult RemoveUserFollow(string id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var follow = user.followUser.FirstOrDefault(f=>f.followuserid==id);
            if (follow!=null) { db.FollowUser.Remove(follow); }
            UserData us = db.UserData.Find(id);
            us.number--;
            db.SaveChanges();
            return RedirectToAction("Salers", new { id = id });
        }
        // GET: UserDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDatas/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nickname,Birthday,Sex,Emailtwo,number,content")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.UserData.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userData);
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId()),
                Logins = await UserManager.GetLoginsAsync(User.Identity.GetUserId()),
                Accout = await UserManager.GetEmailAsync(User.Identity.GetUserId()),
                addtime = UserManager.GetUserAddtime(User.Identity.GetUserId()),
                Nickname = UserManager.GetUserNickname(User.Identity.GetUserId()),
                Birthday = UserManager.GetUserBirthday(User.Identity.GetUserId()),
                Sex = UserManager.GetSex(User.Identity.GetUserId()),
                ImageUrl = UserManager.GetImg(User.Identity.GetUserId())
            };
            return PartialView("_UserDataEdit", model);
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        // POST: UserDatas/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IndexViewModel model, HttpPostedFileBase upload)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                UserData us = (from m in db.UserData where m.id == user.Id select m).SingleOrDefault();
                us.Sex = model.Sex;
                if (model.Birthday == null) { } else {
                    us.Birthday = model.Birthday;
                }
                ApplicationUser u = db.Users.Find(User.Identity.GetUserId());
                db.SaveChanges();
                if (upload != null)
                {
                    if (u.userFile.Count() != 0)
                    {
                        UserFile isnull = db.UserFile.FirstOrDefault(f => f.User.Id == user.Id);
                        string ImageName1 = System.IO.Path.GetFileName(upload.FileName);
                        int length = upload.ContentLength;
                        byte[] buffer = new byte[length];
                        upload.InputStream.Read(buffer, 0, length);
                        isnull.Name = ImageName1;
                        isnull.MimeType = upload.ContentType;
                        isnull.Size = upload.ContentLength;
                        isnull.Content = buffer;
              
                    }
                    else { 
                        string ImageName =Path.GetFileName(upload.FileName);
                        int length = upload.ContentLength;
                        byte[] buffer = new byte[length];
                        upload.InputStream.Read(buffer , 0 , length);
                        var newfile = new UserFile
                        {
                            Name = ImageName,
                            MimeType = upload.ContentType,
                            Size = upload.ContentLength,
                            Content = buffer
                        };
                         u.userFile = new List<UserFile> { newfile };
                    }
                 
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Manage");
            }

            return RedirectToAction("Edit");
        }

        public ActionResult Cancel()
        {
            var models = new IndexViewModel
            {
                HasPassword = HasPassword(),
              
                TwoFactor = UserManager.GetTwoFactorEnabled(User.Identity.GetUserId()),
                Logins = UserManager.GetLogins(User.Identity.GetUserId()),
                Accout = UserManager.GetEmail(User.Identity.GetUserId()),
                addtime = UserManager.GetUserAddtime(User.Identity.GetUserId()),
                Nickname = UserManager.GetUserNickname(User.Identity.GetUserId()),
                Birthday = UserManager.GetUserBirthday(User.Identity.GetUserId()),
                Sex = UserManager.GetSex(User.Identity.GetUserId()),
                id = User.Identity.GetUserId(),
                ImageUrl = UserManager.GetImg(User.Identity.GetUserId())
            };
            return PartialView("_ManageIndex", models);
        }
        // GET: UserDatas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.UserData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserData userData = db.UserData.Find(id);
            db.UserData.Remove(userData);
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
