namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "createtime", c => c.DateTime());
            AlterColumn("dbo.Comment_Member", "addtime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment_Member", "addtime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "createtime", c => c.DateTime(nullable: false));
        }
    }
}
