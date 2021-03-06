namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "City", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "City");
        }
    }
}
