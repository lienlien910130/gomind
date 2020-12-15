namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "Talk", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "Talk");
        }
    }
}
