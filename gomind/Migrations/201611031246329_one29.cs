namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one29 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserDatas", "Emailtwo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDatas", "Emailtwo", c => c.String(maxLength: 255));
        }
    }
}
