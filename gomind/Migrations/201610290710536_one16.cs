namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Name", c => c.String());
            AddColumn("dbo.Files", "MimeType", c => c.String());
            AddColumn("dbo.Files", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "Content", c => c.Binary());
            AddColumn("dbo.Files", "number", c => c.Int(nullable: false));
            DropColumn("dbo.Files", "ImageUrl");
            DropColumn("dbo.Files", "ImageUrl1");
            DropColumn("dbo.Files", "ImageUrl2");
            DropColumn("dbo.Files", "ImageUrl3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "ImageUrl3", c => c.String());
            AddColumn("dbo.Files", "ImageUrl2", c => c.String());
            AddColumn("dbo.Files", "ImageUrl1", c => c.String());
            AddColumn("dbo.Files", "ImageUrl", c => c.String(maxLength: 255));
            DropColumn("dbo.Files", "number");
            DropColumn("dbo.Files", "Content");
            DropColumn("dbo.Files", "Size");
            DropColumn("dbo.Files", "MimeType");
            DropColumn("dbo.Files", "Name");
        }
    }
}
