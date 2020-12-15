namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one24 : DbMigration
    {
        public override void Up()
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
            
            DropColumn("dbo.Settings", "NewPFollow");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "NewPFollow", c => c.Boolean(nullable: false));
            DropTable("dbo.Contacts");
        }
    }
}
