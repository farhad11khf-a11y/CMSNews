namespace CMSNews.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.T-User", "Role", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.T-User", "Role",
    c => c.String(nullable: false, maxLength: 20, defaultValue: "User"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T-User", "Role");
        }
    }
}
