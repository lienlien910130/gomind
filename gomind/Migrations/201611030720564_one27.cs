namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one27 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comment_Member", "score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment_Member", "score", c => c.Single(nullable: false));
        }
    }
}
