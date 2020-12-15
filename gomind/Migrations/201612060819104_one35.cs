namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductLists", "count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductLists", "count");
        }
    }
}
