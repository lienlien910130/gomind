namespace gomind.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        toUserId = c.String(),
                        toUserName = c.String(),
                        ProductList_number = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductLists", t => t.ProductList_number)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProductList_number)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Message = c.String(),
                        addtime = c.DateTime(nullable: false),
                        Chat_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Chats", t => t.Chat_ID)
                .Index(t => t.Chat_ID);
            
            CreateTable(
                "dbo.ProductLists",
                c => new
                    {
                        number = c.Guid(nullable: false),
                        userid = c.String(maxLength: 128),
                        plistsale_mnumber = c.String(nullable: false),
                        prodlist_status = c.String(nullable: false, maxLength: 256),
                        prodlist_name = c.String(nullable: false, maxLength: 256),
                        prodlist_price = c.Int(nullable: false),
                        prodlist_content = c.String(maxLength: 256),
                        createdate = c.DateTime(nullable: false),
                        prodlist_followtimes = c.Int(),
                        onekind = c.String(),
                        twokind = c.String(),
                        url = c.String(),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.Comment_Product",
                c => new
                    {
                        number = c.Int(nullable: false, identity: true),
                        snumber = c.String(maxLength: 256),
                        bnumberid = c.String(maxLength: 256),
                        bnumbername = c.String(maxLength: 256),
                        prccont = c.String(nullable: false, maxLength: 256),
                        prcscore = c.Single(nullable: false),
                        prctime = c.DateTime(nullable: false),
                        ProductList_number = c.Guid(),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.ProductLists", t => t.ProductList_number)
                .Index(t => t.ProductList_number);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 255),
                        ImageUrl1 = c.String(),
                        ImageUrl2 = c.String(),
                        ImageUrl3 = c.String(),
                        ProductList_number = c.Guid(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.ProductLists", t => t.ProductList_number)
                .Index(t => t.ProductList_number);
            
            CreateTable(
                "dbo.FollowTables",
                c => new
                    {
                        number = c.Int(nullable: false, identity: true),
                        productnumber = c.Guid(nullable: false),
                        ProductList_number = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.ProductLists", t => t.ProductList_number)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProductList_number)
                .Index(t => t.User_Id);
            
           
            
            CreateTable(
                "dbo.Comment_Member",
                c => new
                    {
                        mecom_number = c.String(nullable: false, maxLength: 256),
                        mecritic = c.String(maxLength: 256),
                        mecobj = c.String(maxLength: 256),
                        meccont = c.String(maxLength: 20),
                        mecscore = c.Single(nullable: false),
                        mectime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.mecom_number)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.FollowUsers",
                c => new
                    {
                        number = c.Int(nullable: false, identity: true),
                        userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.number)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid);
            
          
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        buyerid = c.String(),
                        createtime = c.DateTime(nullable: false),
                        pay = c.Boolean(nullable: false),
                        send = c.Boolean(nullable: false),
                        ProductList_number = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ProductLists", t => t.ProductList_number)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProductList_number)
                .Index(t => t.User_Id);
          
            CreateTable(
                "dbo.SaleSettings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SendFace = c.Boolean(nullable: false),
                        SendATM = c.Boolean(nullable: false),
                        SendHome = c.Boolean(nullable: false),
                        SendSeven = c.Boolean(nullable: false),
                        SendFamily = c.Boolean(nullable: false),
                        SendPost = c.Boolean(nullable: false),
                        HomeMoney = c.Int(),
                        SevenMoney = c.Int(),
                        FamilMoney = c.Int(),
                        PostMoney = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NewComment = c.Boolean(nullable: false),
                        NewFollower = c.Boolean(nullable: false),
                        NewProComment = c.Boolean(nullable: false),
                        NewTalk = c.Boolean(nullable: false),
                        NewPFollow = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Nickname = c.String(maxLength: 255),
                        Birthday = c.DateTime(),
                        Sex = c.String(maxLength: 255),
                        Emailtwo = c.String(maxLength: 255),
                        number = c.Int(),
                        content = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(maxLength: 255),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.One",
                c => new
                    {
                        OneId = c.Int(nullable: false, identity: true),
                        OneKind = c.String(),
                    })
                .PrimaryKey(t => t.OneId);
            
         
            CreateTable(
                "dbo.Twoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OneId = c.Int(nullable: false),
                        TwoKind = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserFiles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDatas", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Settings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleSettings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductLists", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ProductList_number", "dbo.ProductLists");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowUsers", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowTables", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment_Member", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Chats", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowTables", "ProductList_number", "dbo.ProductLists");
            DropForeignKey("dbo.Files", "ProductList_number", "dbo.ProductLists");
            DropForeignKey("dbo.Comment_Product", "ProductList_number", "dbo.ProductLists");
            DropForeignKey("dbo.Chats", "ProductList_number", "dbo.ProductLists");
            DropForeignKey("dbo.ChatMessages", "Chat_ID", "dbo.Chats");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserFiles", new[] { "User_Id" });
            DropIndex("dbo.UserDatas", new[] { "User_Id" });
            DropIndex("dbo.Settings", new[] { "User_Id" });
            DropIndex("dbo.SaleSettings", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "ProductList_number" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.FollowUsers", new[] { "userid" });
            DropIndex("dbo.Comment_Member", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FollowTables", new[] { "User_Id" });
            DropIndex("dbo.FollowTables", new[] { "ProductList_number" });
            DropIndex("dbo.Files", new[] { "ProductList_number" });
            DropIndex("dbo.Comment_Product", new[] { "ProductList_number" });
            DropIndex("dbo.ProductLists", new[] { "userid" });
            DropIndex("dbo.ChatMessages", new[] { "Chat_ID" });
            DropIndex("dbo.Chats", new[] { "User_Id" });
            DropIndex("dbo.Chats", new[] { "ProductList_number" });
            DropTable("dbo.Twoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.One");
            DropTable("dbo.UserFiles");
            DropTable("dbo.UserDatas");
            DropTable("dbo.Settings");
            DropTable("dbo.SaleSettings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FollowUsers");
            DropTable("dbo.Comment_Member");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FollowTables");
            DropTable("dbo.Files");
            DropTable("dbo.Comment_Product");
            DropTable("dbo.ProductLists");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.Chats");
        }
    }
}
