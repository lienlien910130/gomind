namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one18 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FollowUsers", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FollowUsers", "ImageUrl", c => c.Boolean(nullable: false));
        }
    }
}
