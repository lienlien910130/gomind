namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.FollowUsers", new[] { "userid" });
            AddColumn("dbo.FollowUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.FollowUsers", "userid", c => c.String());
            CreateIndex("dbo.FollowUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.FollowUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowUsers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.FollowUsers", "userid", c => c.String(maxLength: 128));
            DropColumn("dbo.FollowUsers", "ApplicationUser_Id");
            CreateIndex("dbo.FollowUsers", "userid");
            AddForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers", "Id");
        }
    }
}
