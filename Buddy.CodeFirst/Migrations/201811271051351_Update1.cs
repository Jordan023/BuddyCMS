namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserGroup_Id", c => c.Int());
            AddColumn("dbo.Users", "UserLogin_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserGroup_Id");
            CreateIndex("dbo.Users", "UserLogin_Id");
            AddForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups", "Id");
            AddForeignKey("dbo.Users", "UserLogin_Id", "dbo.UserLogins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserLogin_Id", "dbo.UserLogins");
            DropForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "UserLogin_Id" });
            DropIndex("dbo.Users", new[] { "UserGroup_Id" });
            DropColumn("dbo.Users", "UserLogin_Id");
            DropColumn("dbo.Users", "UserGroup_Id");
        }
    }
}
