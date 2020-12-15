namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment_Member", "score", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment_Member", "score");
        }
    }
}
