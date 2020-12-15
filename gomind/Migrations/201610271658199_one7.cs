namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one7 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FollowUsers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.FollowUsers", "userid");
            RenameColumn(table: "dbo.FollowUsers", name: "ApplicationUser_Id", newName: "userid");
            AddColumn("dbo.FollowUsers", "followuserid", c => c.String());
            AlterColumn("dbo.FollowUsers", "userid", c => c.String(maxLength: 128));
            CreateIndex("dbo.FollowUsers", "userid");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FollowUsers", new[] { "userid" });
            AlterColumn("dbo.FollowUsers", "userid", c => c.String());
            DropColumn("dbo.FollowUsers", "followuserid");
            RenameColumn(table: "dbo.FollowUsers", name: "userid", newName: "ApplicationUser_Id");
            AddColumn("dbo.FollowUsers", "userid", c => c.String());
            CreateIndex("dbo.FollowUsers", "ApplicationUser_Id");
        }
    }
}
