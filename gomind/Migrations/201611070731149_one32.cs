namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chats", "count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chats", "count");
        }
    }
}
