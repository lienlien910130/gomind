namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Addtime");
        }
        
        public override void Down()
        {
        }
    }
}
