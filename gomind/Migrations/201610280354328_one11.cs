namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.Orders", "Chat_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Chat_ID");
            AddForeignKey("dbo.Orders", "Chat_ID", "dbo.Chats", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Chat_ID", "dbo.Chats");
            DropIndex("dbo.Orders", new[] { "Chat_ID" });
            DropColumn("dbo.Orders", "Chat_ID");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
