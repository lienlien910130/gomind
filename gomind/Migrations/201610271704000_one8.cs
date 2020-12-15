namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.FollowUsers", new[] { "userid" });
            DropTable("dbo.FollowUsers");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.number);
            
            CreateIndex("dbo.FollowUsers", "userid");
            AddForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers", "Id");
        }
    }
}
