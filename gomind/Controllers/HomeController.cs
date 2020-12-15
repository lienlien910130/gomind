using gomind.Models;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
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
        private int pageSize = 9;
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
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
            var q = db.ProductList.OrderByDescending(f => f.createdate);
            var result = q.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        [HttpGet]
        public ActionResult PartialIndex(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
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
            var q = db.ProductList.OrderByDescending(f => f.createdate);
            var result = q.ToPagedList(currentPage, pageSize);
            return PartialView("_PagedList",result);
        }
        //[HttpPost]
        //public ActionResult Index(ProductList p)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>()
        //    {
        //        new SelectListItem() { Text = "a", Value = "1"},
        //        new SelectListItem() { Text = "b", Value = "2"},
        //        new SelectListItem() { Text = "c", Value = "3"},
        //    };
        //    ViewBag.Item = list;
        //    return View(p);
        //}
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult GetCountries()
        {
            return Json(db.One.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetStatesByCountryId(string OneId)
        {
            if (OneId == "")
            {
                return Json(1);
            }
            int Id = Convert.ToInt32(OneId);
            var two = from a in db.Two where a.OneId == Id select a;
            return Json(two, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetTwo(string OneId) {
            int Id = Convert.ToInt32(OneId);
            var two = from a in db.Two where a.OneId == Id select a;
            return Json(two, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChat(string id)
        {
            if (id == "") { return Json(1, JsonRequestBehavior.AllowGet); }
            else {
            var d = db.Setting.FirstOrDefault(f => f.User.Id == id);
                //var n = db.Setting.FirstOrDefault(f=>f.User.Id == id).Talk;
                //var t = db.Chat.Where(f=>f.toUserId == id && f.ChatMessage.Count()>0).Count();
                //if (n != t)
                //{
                //        var c = db.Chat.Last(f => f.toUserId == id && f.ChatMessage.Count() > 0);
                //        d.Talk = t;
                //        db.SaveChanges();
                //        return Json(c.ID, JsonRequestBehavior.AllowGet);
                //}
                var q = db.Chat.Where(f => f.toUserId == id && f.ChatMessage.Count() > 0).ToList();
                var result = new LinkedList<object>();
                foreach (var i in q)
                {
                    if (i.ChatMessage.Count() != i.count)
                    {
                       result.AddLast(new
                       {
                              ID = i.ID
                       });
                        i.count = i.ChatMessage.Count();
                        db.SaveChanges();
                    }
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //var two = db.Chat.FirstOrDefault(f=>f.==id);
            //return Json(two.ID, JsonRequestBehavior.AllowGet);

        }
    }
}
