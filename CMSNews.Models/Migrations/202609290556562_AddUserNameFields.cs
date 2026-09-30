namespace CMSNews.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T-User", "FirstName",
                c => c.String(nullable: false, maxLength: 30, defaultValue: "نام"));

            AddColumn("dbo.T-User", "LastName",
                c => c.String(nullable: false, maxLength: 50, defaultValue: "نام خانوادگی"));
        }

        public override void Down()
        {
            DropColumn("dbo.T-User", "LastName");
            DropColumn("dbo.T-User", "FirstName");
        }
    }
}
