namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one34 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SaleSettings", "SendATM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleSettings", "SendATM", c => c.Boolean(nullable: false));
        }
    }
}
