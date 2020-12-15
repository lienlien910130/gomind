namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "getco", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "givco", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "givco");
            DropColumn("dbo.Orders", "getco");
        }
    }
}
