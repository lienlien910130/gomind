namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFiles", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserFiles", "Content");
        }
    }
}
