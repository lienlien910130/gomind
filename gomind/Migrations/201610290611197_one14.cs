namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one14 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserFiles", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFiles", "Content", c => c.Byte(nullable: false));
        }
    }
}
