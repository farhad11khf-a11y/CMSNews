namespace CMSNews.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T-User", "ImageName", c => c.String(nullable: false, maxLength: 200,
            defaultValue: "nophoto.png"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T-User", "ImageName");
        }
    }
}
