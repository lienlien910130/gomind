namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one19 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Comment_Member");
            AddColumn("dbo.Comment_Member", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Comment_Member", "tousername", c => c.String());
            AddColumn("dbo.Comment_Member", "touserid", c => c.String());
            AddColumn("dbo.Comment_Member", "ccont", c => c.String());
            AddColumn("dbo.Comment_Member", "score", c => c.Single(nullable: false));
            AddColumn("dbo.Comment_Member", "addtime", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Comment_Member", "id");
            DropColumn("dbo.Comment_Member", "mecom_number");
            DropColumn("dbo.Comment_Member", "mecritic");
            DropColumn("dbo.Comment_Member", "mecobj");
            DropColumn("dbo.Comment_Member", "meccont");
            DropColumn("dbo.Comment_Member", "mecscore");
            DropColumn("dbo.Comment_Member", "mectime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment_Member", "mectime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comment_Member", "mecscore", c => c.Single(nullable: false));
            AddColumn("dbo.Comment_Member", "meccont", c => c.String(maxLength: 20));
            AddColumn("dbo.Comment_Member", "mecobj", c => c.String(maxLength: 256));
            AddColumn("dbo.Comment_Member", "mecritic", c => c.String(maxLength: 256));
            AddColumn("dbo.Comment_Member", "mecom_number", c => c.String(nullable: false, maxLength: 256));
            DropPrimaryKey("dbo.Comment_Member");
            DropColumn("dbo.Comment_Member", "addtime");
            DropColumn("dbo.Comment_Member", "score");
            DropColumn("dbo.Comment_Member", "ccont");
            DropColumn("dbo.Comment_Member", "touserid");
            DropColumn("dbo.Comment_Member", "tousername");
            DropColumn("dbo.Comment_Member", "id");
            AddPrimaryKey("dbo.Comment_Member", "mecom_number");
        }
    }
}
