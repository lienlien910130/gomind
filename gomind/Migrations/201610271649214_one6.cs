namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment_Product", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comment_Product", "User_Id");
            AddForeignKey("dbo.Comment_Product", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comment_Product", "snumber");
            DropColumn("dbo.Comment_Product", "bnumberid");
            DropColumn("dbo.Comment_Product", "bnumbername");
            DropColumn("dbo.Comment_Product", "prcscore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment_Product", "prcscore", c => c.Single(nullable: false));
            AddColumn("dbo.Comment_Product", "bnumbername", c => c.String(maxLength: 256));
            AddColumn("dbo.Comment_Product", "bnumberid", c => c.String(maxLength: 256));
            AddColumn("dbo.Comment_Product", "snumber", c => c.String(maxLength: 256));
            DropForeignKey("dbo.Comment_Product", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comment_Product", new[] { "User_Id" });
            DropColumn("dbo.Comment_Product", "User_Id");
        }
    }
}
