namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one30 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FollowTables", "productnumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FollowTables", "productnumber", c => c.Guid(nullable: false));
        }
    }
}
