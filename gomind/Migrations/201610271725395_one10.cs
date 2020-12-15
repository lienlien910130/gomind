namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FollowUsers", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FollowUsers", "ImageUrl");
        }
    }
}
