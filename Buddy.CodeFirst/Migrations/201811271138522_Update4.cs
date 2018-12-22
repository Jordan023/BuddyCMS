namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogins", "PasswordSalt", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogins", "PasswordSalt");
        }
    }
}
