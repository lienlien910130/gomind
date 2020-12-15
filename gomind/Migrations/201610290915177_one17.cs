namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FollowUsers", "ImageUrl", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FollowUsers", "ImageUrl", c => c.String());
        }
    }
}
