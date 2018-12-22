namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLogins", "PasswordHash", c => c.String(maxLength: 100));
            DropColumn("dbo.UserLogins", "PasswordSalt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserLogins", "PasswordSalt", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserLogins", "PasswordHash", c => c.String(maxLength: 50));
        }
    }
}
