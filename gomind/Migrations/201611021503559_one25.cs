namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "NewOrder", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Settings", "TalkNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "NewComment");
            DropColumn("dbo.Settings", "NewFollower");
            DropColumn("dbo.Settings", "NewProComment");
            DropTable("dbo.Contacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        ContactName = c.String(),
                        ContactNo = c.String(),
                        AddedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactID);
            
            AddColumn("dbo.Settings", "NewProComment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "NewFollower", c => c.Boolean(nullable: false));
            AddColumn("dbo.Settings", "NewComment", c => c.Boolean(nullable: false));
            DropColumn("dbo.Settings", "TalkNumber");
            DropColumn("dbo.Settings", "OrderNumber");
            DropColumn("dbo.Settings", "NewOrder");
        }
    }
}
