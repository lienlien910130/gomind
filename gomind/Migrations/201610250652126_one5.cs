namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FollowUsers", "username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FollowUsers", "username");
        }
    }
}
