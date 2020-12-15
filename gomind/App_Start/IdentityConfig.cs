using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using gomind.Models;

namespace IdentitySample.Models
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
        internal bool SearchUser(string id, Guid? proid)
        {

            ApplicationUser us = (from m in db.Users where m.Id == id select m).SingleOrDefault();
            ProductList productList = db.ProductList.Find(proid);
            if (productList != null)
            {
                if (productList.userid == us.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        internal bool SearchPFollow(string id, Guid? proid)
        {

            ApplicationUser us = (from m in db.Users where m.Id == id select m).SingleOrDefault();
            if (us.followTable.Where(f => f.ProductList.number== proid).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal DateTime? GetUserAddtime(string id)
        {

            try
            {
                DateTime addtime = (from m in db.Users where m.Id == id select m.Useraddtime).SingleOrDefault();
                return addtime;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        internal bool GetImg(string id)
        {

            try
            {
                var file = db.UserFile.Where(f => f.User.Id == id).SingleOrDefault();
                if (file != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        internal bool SearchUSollow(string id, string uid)
        {

            try
            {
                ApplicationUser us = (from m in db.Users where m.Id == id select m).SingleOrDefault();
                if (us.followUser.Where(f => f.followuserid == uid).Count() >0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }


      
        internal string GetSex(string id)
        {
            try
            {
                string sex = (from m in db.UserData where m.id == id select m.Sex).SingleOrDefault();
                return sex;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        internal DateTime? GetUserBirthday(string id)
        {
            try
            {
                DateTime? birthday = (from m in db.UserData where m.id == id select m.Birthday).SingleOrDefault();
                return birthday;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        internal string GetUserNickname(string id)
        {
            try
            {
                UserData us = db.UserData.FirstOrDefault(f => f.id == id);
                if (us != null)
                {
                    string nickname = (from m in db.UserData where m.id == id select m.Nickname).SingleOrDefault();
                    return nickname;
                }
                else
                {
                    string a = GetUsersname(id);
                    return a;
                }


            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        internal string GetUsersname(string id)
        {
            try
            {
                string nickname = (from m in db.Users where m.Id == id select m.UserNickName).SingleOrDefault();
                return nickname;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole,string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
  /*  public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> 
    {
        protected override void Seed(ApplicationDbContext context) {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db) {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null) {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null) {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name)) {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
*/
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : 
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}