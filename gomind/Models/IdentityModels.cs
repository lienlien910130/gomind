using gomind.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
      
        public DateTime Useraddtime { get; set; }
        public string UserNickName { get; set; }

        public virtual ICollection<Comment_Member> comment_Member { get; set; }
        public virtual ICollection<FollowTable> followTable { get; set; }
        public virtual ICollection<FollowUser> followUser { get; set; }
        public virtual ICollection<Order> order { get; set; }
        public virtual ICollection<ProductList> productList { get; set; }
        public virtual ICollection<UserData> userData { get; set; }
        public virtual ICollection<SaleSetting> saleSetting { get; set; }
        public virtual ICollection<Setting> setting { get; set; }
        public virtual ICollection<UserFile> userFile { get; set; }
        public virtual ICollection<Chat> chat { get; set; }
        public virtual ICollection<Comment_Product> comment_Product { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<FollowTable> FollowTable { get; set; }
        public DbSet<FollowUser> FollowUser { get; set; }
        public DbSet<Comment_Member> Comment_Member { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<Comment_Product> Comment_Product { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<UserFile> UserFile { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<SaleSetting> SaleSetting { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<One> One { get; set; }
        public DbSet<Two> Two { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }

        /*      static ApplicationDbContext()
              {
                  // Set the database intializer which is run once during application start
                  // This seeds the database with admin user credentials and admin role
                  Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
              }
      */
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}