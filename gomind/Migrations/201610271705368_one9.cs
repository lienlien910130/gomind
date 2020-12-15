namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowUsers",
                c => new
                    {
                        number = c.Int(nullable: false, identity: true),
                        userid = c.String(maxLength: 128),
                        username = c.String(),
                        followuserid = c.String(),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.FollowUsers", new[] { "userid" });
            DropTable("dbo.FollowUsers");
        }
    }
}
