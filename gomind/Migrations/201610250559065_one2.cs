namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Useraddtime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserNickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserNickName");
            DropColumn("dbo.AspNetUsers", "Useraddtime");
        }
    }
}
