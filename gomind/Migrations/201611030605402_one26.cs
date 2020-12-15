namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "name", c => c.String());
            DropColumn("dbo.Settings", "NewOrder");
            DropColumn("dbo.Settings", "NewTalk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "NewTalk", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "NewOrder", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "name");
        }
    }
}
