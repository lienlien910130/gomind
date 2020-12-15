using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{

    public class UsersAdminController : Controller
    {
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
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

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
             
            var user = await UserManager.FindByIdAsync(id);
            if (user.setting!=null) {  db.Setting.FirstOrDefault(f => f.User.Id == id);}
            if (user.saleSetting!=null) { db.SaleSetting.FirstOrDefault(f => f.User.Id == id); }
            if (user.userData != null) { db.UserData.FirstOrDefault(f => f.User.Id == id); }
            if (user.userFile != null) { db.UserFile.FirstOrDefault(f => f.User.Id == id); }
            if (user.productList.Count() > 0)
            {
                var productList = db.ProductList.Where(f => f.User.Id == id).ToList();
                foreach (var l in productList) { 
                if (l.File != null)
                {
                    var fi = db.File.Where(f => f.ProductList.number == l.number).ToList();
                    foreach (var i in fi) { db.File.Remove(i); }
                }
                if (l.Comment_Product != null)
                {
                    var cop = db.Comment_Product.Where(f => f.ProductList.number == l.number).ToList();
                    foreach (var i in cop) { db.Comment_Product.Remove(i); }
                }
                if (l.FollowTable != null)
                {
                    var ft = db.FollowTable.Where(f => f.ProductList.number == l.number).ToList();
                    foreach (var i in ft) { db.FollowTable.Remove(i); }
                }
                if (l.Chat != null)
                {
                    var ch = db.Chat.Where(f => f.ProductList.number == l.number).ToList();
                    foreach (var i in ch)
                    {
                        if (i.ChatMessage.Count() > 0)
                        {
                            var me = db.ChatMessage.Where(f => f.Chat.ID == i.ID).ToList();
                            foreach (var q in me) { db.ChatMessage.Remove(q); }
                            if (i.Order.Count() > 0)
                            {
                                var or = db.Order.Where(f => f.Chat.ID == i.ID).ToList();
                                foreach (var q in or)
                                {
                                    if (q.Comment_Member.Count() > 0)
                                    {
                                        var cme = db.Comment_Member.Where(f => f.Order.id == q.id).ToList();
                                        foreach (var qs in cme) { db.Comment_Member.Remove(qs); }
                                    }
                                    db.Order.Remove(q);
                                }
                            }
                        }
                        db.Chat.Remove(i);
                    }
                }
                }
            }
            if (user.order.Count()>0)
            {
                var order = db.Order.Where(f=>f.User.Id==id).ToList();
                foreach (var x in order)
                {
                    if (x.Comment_Member.Count > 0 || x.Comment_Member != null)
                    {
                        var co = db.Comment_Member.Where(f => f.Order.id == x.id).ToList();
                        foreach (var xa in co) { db.Comment_Member.Remove(xa); }
                    }
                    db.Order.Remove(x);
                }
                var ord = db.Order.Where(f => f.buyerid == id);
                foreach (var e in ord)
                {
                    if (e.Comment_Member.Count > 0 || e.Comment_Member != null)
                    {
                        var co = db.Comment_Member.Where(f => f.Order.id == e.id).ToList();
                        foreach (var xa in co) { db.Comment_Member.Remove(xa); }
                    }
                    db.Order.Remove(e);
                }
            }
            if (user.followUser.Count() > 0)
            {
                var i = db.FollowUser.Where(f=>f.User.Id==id).ToList();
                foreach (var o in i) { db.FollowUser.Remove(o); }
                var ii = db.FollowUser.Where(f => f.followuserid == id).ToList();
                foreach (var o in ii) { db.FollowUser.Remove(o); }
            }
            if (user.followTable.Count()>0) {
                var followTable = db.FollowTable.Where(f=>f.User.Id==id);
                foreach (var a in followTable) { 
                db.FollowTable.Remove(a);}
            }
            if (user.comment_Member.Count()>0) {
                var co = db.Comment_Member.Where(f => f.User.Id == id);
                foreach (var a in co)
                {
                    db.Comment_Member.Remove(a);
                }
            }
            if (user.comment_Product.Count()>0)
            {
                var co = db.Comment_Product.Where(f => f.User.Id == id);
                foreach (var a in co)
                {
                    db.Comment_Product.Remove(a);
                }
            }
            if (user.chat.Count()>0)
            {
                var co = db.Chat.Where(f => f.User.Id == id);
                foreach (var a in co)
                {
                    db.Chat.Remove(a);
                }
            }
            var result = await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
