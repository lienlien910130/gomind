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
    public class ChatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ChatsController()
        {
        }

        public ChatsController(ApplicationUserManager userManager)
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
        // GET: Chats
        public ActionResult Index()
        {
            return View(db.Chat.ToList());
        }
        [Authorize]
        public ActionResult ChatIndex(string id, Guid pid) {
       
            string a = User.Identity.GetUserId();
            var u = db.Users.FirstOrDefault(f => f.Id == a);
            if (a == id) {
                return RedirectToAction("Details","ProductLists",new { id=pid});
            }
            var s = db.Chat.FirstOrDefault(f => f.User.Id == a && f.toUserId == id && f.ProductList.number == pid);
            ChatIndex chatindex = new ChatIndex();
            if (s == null)
            {
                var newchat = new Chat()
                {
                    toUserId = id,
                    toUserName = db.Users.FirstOrDefault(f => f.Id == id).UserNickName,
                    UserName = u.UserNickName,
                    ProductList = db.ProductList.Find(pid),
                    count = 0
                };
                u.chat = new List<Chat> { newchat };
                chatindex.Chat = newchat;
                db.Chat.Add(newchat);
                db.SaveChanges();
            }
            else { chatindex.Chat = s; }
            var ss = db.Chat.FirstOrDefault(f => f.User.Id == a && f.toUserId == id && f.ProductList.number == pid);
            var q = db.Order.FirstOrDefault(f => f.Chat.ID == ss.ID && f.buyerid == a);
             if (q != null)
            {
                chatindex.IsOrder = true;
            }
            else { chatindex.IsOrder = false; }
            if (ss.toUserId == a)
            {
                chatindex.IsUser = true;
            }
            else { chatindex.IsUser = false; }
            var i = db.Order.FirstOrDefault(f=>f.Chat.ID == ss.ID);
            if (i != null) { chatindex.IsSale = true; }
            else { chatindex.IsSale = false; }
            chatindex.ImageUrl = UserManager.GetImg(id);
            Session["chatids"] = User.Identity.GetUserId();
            return View(chatindex);

    }
        public ActionResult Chats(string id, Guid pid) {

           var s = db.Chat.Where(f => f.ProductList.number == pid);
            if (s == null)
            {
                return View("Details","ProductLists",new { id=pid});
            }
            else {
                return View(s.ToList());
            }

        }
        public ActionResult ChatDetail(int id)
        {
            string a = User.Identity.GetUserId();
            var u = db.Users.FirstOrDefault(f => f.Id == a);
            var s = db.Chat.FirstOrDefault(f => f.ID==id);
            if (s == null)
            {
                return View();
            }
            else {
                ChatIndex chatindex = new ChatIndex();
                chatindex.Chat = s;
                if (s.ChatMessage != null)
                {
                    chatindex.ChatMessage = s.ChatMessage.ToList();
                }
                else { chatindex.Message = new ChatMessage(); }
                if (db.Order.FirstOrDefault(f => f.Chat.ID == s.ID ) !=null)
                {
                    chatindex.IsOrder = true;
                }
                else { chatindex.IsOrder = false; }
                if (s.toUserId == a)
                {
                    chatindex.IsUser = true;
                }
                else { chatindex.IsUser = false; }
                Session["chatid"] = id;
                return View(chatindex);
            }

        }
        public ActionResult My()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            Manage models = new Manage();
            models.Chat = db.Chat.Where(f => f.toUserId == user.Id || f.User.Id == user.Id).ToList();
            return View(models);
        }
        public ActionResult btnchat(int id) {
            string a = User.Identity.GetUserId();
            var u = db.Users.FirstOrDefault(f => f.Id == a);
            var s = db.Chat.FirstOrDefault(f => f.ID == id);
            if (s == null)
            {
                return View();
            }
            else
            {
                ChatIndex chatindex = new ChatIndex();
                chatindex.Chat = s;
                if (s.ChatMessage != null)
                {
                    chatindex.ChatMessage = s.ChatMessage.ToList();
                }
                else { chatindex.Message = new ChatMessage(); }
                if (db.Order.FirstOrDefault(f => f.Chat.ID == s.ID) != null)
                {
                    chatindex.IsOrder = true;
                    Session["IsOrder"] = true;
                }
                else { chatindex.IsOrder = false;
                    Session["IsOrder"] = false;
                }
                if (s.toUserId == a)
                {
                    chatindex.IsUser = true;
                    Session["IsUser"] = true;
                }
                else { chatindex.IsUser = false;
                    Session["IsUser"] = false;
                }
                var q = db.Chat.Where(f => f.toUserId == a && f.ChatMessage.Count() > 0).ToList();
                var ccc = db.Chat.Find(id);
                ccc.count = ccc.ChatMessage.Count();
                db.SaveChanges();
                //foreach (var i in q)
                //{
                //    if (i.ChatMessage.Count() != i.count)
                //    {
                //        i.count = i.ChatMessage.Count();
                //        db.SaveChanges();
                //    }
                //}
                Session["chatid"] = id;
                return View(chatindex);
            }


        }
        // GET: Chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string message, int id )
        {
           string a = User.Identity.GetUserId();
           var u = db.Users.FirstOrDefault(f => f.Id == a);
           var s = db.Chat.FirstOrDefault(f => f.ID == id);
           var m = new ChatMessage
           {
                    Message = message,
                    addtime = DateTime.Now,
                    UserName = u.UserNickName
           };
           s.ChatMessage = new List<ChatMessage> { m };
           db.SaveChanges();
           return RedirectToAction("Chat", new { id = s.ID });
      
        }
        public ActionResult Chat(int id) {

            var s = db.Chat.FirstOrDefault(f => f.ID == id);
            ChatIndex chatindex = new ChatIndex();
            chatindex.Chat = s;
            chatindex.ChatMessage = s.ChatMessage.ToList();
            return PartialView("_Chat",chatindex);

        }
        // GET: Chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,toUserId,toUserName")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chat);
        }

        // GET: Chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chat.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chat chat = db.Chat.Find(id);
            if (chat.ChatMessage!=null)
            {
                var mess = db.ChatMessage.Where(f=>f.Chat.ID==id);
                if (mess != null)
                {
                    foreach (var i in mess) { db.ChatMessage.Remove(i); }
                }
                db.SaveChanges();
            }
            if (chat.Order!=null)
            {
                var order = db.Order.FirstOrDefault(f=>f.Chat.ID==id);
                if (order!=null) {
                    var comm = db.Comment_Member.Where(f => f.Order.id == order.id);
                    if (comm!=null) {
                        foreach (var i in comm) {  db.Comment_Member.Remove(i);}
                    }
                    db.Order.Remove(order);
                }
                db.SaveChanges();
            }
            db.Chat.Remove(chat);
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


        public JsonResult GetMessages(int id)
        {
            var s = db.Chat.FirstOrDefault(f => f.ID == id);
            ChatIndex chatindex = new ChatIndex();
            chatindex.Chat = s;
            chatindex.ChatMessage = s.ChatMessage.ToList();
            var messages = db.ChatMessage.Where(f=>f.Chat.ID==id).OrderBy(x => x.addtime);
            var result = new LinkedList<object>();
            foreach (var mes in messages)
            {
                result.AddLast(new
                {
                    Username = mes.UserName,
                    PostDateTime = mes.addtime.ToString(),
                    MessageBody = mes.Message
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddMessage(int id, string MessageBody)
        {
            string a = User.Identity.GetUserId();
            var u = db.Users.FirstOrDefault(f => f.Id == a);
            var s = db.Chat.FirstOrDefault(f => f.ID == id);
            var m = new ChatMessage
            {
                Message = MessageBody,
                addtime = DateTime.Now,
                UserName = u.UserNickName
            };
            s.ChatMessage = new List<ChatMessage> { m };
            db.SaveChanges();
            var result = new { message = "Success" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
