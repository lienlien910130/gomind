namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ProductList_number", "dbo.ProductLists");
            DropIndex("dbo.Orders", new[] { "ProductList_number" });
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            DropColumn("dbo.Orders", "ProductList_number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductList_number", c => c.Guid());
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Orders", "ProductList_number");
            AddForeignKey("dbo.Orders", "ProductList_number", "dbo.ProductLists", "number");
        }
    }
}
