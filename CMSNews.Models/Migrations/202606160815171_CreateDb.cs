namespace CMSNews.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 2000),
                        Name = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 30),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.T-News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.T-News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(nullable: false, maxLength: 300),
                        Description = c.String(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 100),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        see = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        NewsGroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.T_ NewsGroup", t => t.NewsGroupId, cascadeDelete: true)
                .ForeignKey("dbo.T-User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NewsGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_ NewsGroup",
                c => new
                    {
                        NewsGroupId = c.Int(nullable: false),
                        NewsGroupTitle = c.String(nullable: false, maxLength: 200),
                        ImageName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.NewsGroupId);
            
            CreateTable(
                "dbo.T-User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 100),
                        RegisteDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Comment", "NewsId", "dbo.T-News");
            DropForeignKey("dbo.T-News", "UserId", "dbo.T-User");
            DropForeignKey("dbo.T-News", "NewsGroupId", "dbo.T_ NewsGroup");
            DropIndex("dbo.T-News", new[] { "UserId" });
            DropIndex("dbo.T-News", new[] { "NewsGroupId" });
            DropIndex("dbo.T_Comment", new[] { "NewsId" });
            DropTable("dbo.T-User");
            DropTable("dbo.T_ NewsGroup");
            DropTable("dbo.T-News");
            DropTable("dbo.T_Comment");
        }
    }
}
