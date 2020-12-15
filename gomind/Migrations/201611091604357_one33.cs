namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment_Member", "edit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment_Member", "edit");
        }
    }
}
