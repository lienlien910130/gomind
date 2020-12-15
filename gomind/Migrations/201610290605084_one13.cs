namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFiles", "Name", c => c.String());
            AddColumn("dbo.UserFiles", "MimeType", c => c.String());
            AddColumn("dbo.UserFiles", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.UserFiles", "Content", c => c.Byte(nullable: false));
            DropColumn("dbo.UserFiles", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFiles", "ImageUrl", c => c.String(maxLength: 255));
            DropColumn("dbo.UserFiles", "Content");
            DropColumn("dbo.UserFiles", "Size");
            DropColumn("dbo.UserFiles", "MimeType");
            DropColumn("dbo.UserFiles", "Name");
        }
    }
}
