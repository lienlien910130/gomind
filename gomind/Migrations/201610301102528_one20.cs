namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment_Member", "Order_id", c => c.Int());
            CreateIndex("dbo.Comment_Member", "Order_id");
            AddForeignKey("dbo.Comment_Member", "Order_id", "dbo.Orders", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment_Member", "Order_id", "dbo.Orders");
            DropIndex("dbo.Comment_Member", new[] { "Order_id" });
            DropColumn("dbo.Comment_Member", "Order_id");
        }
    }
}
