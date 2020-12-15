namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Addtime", c => c.DateTime(nullable: false));
           
        }
        
        public override void Down()
        {
             DropColumn("dbo.AspNetUsers", "Addtime");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
