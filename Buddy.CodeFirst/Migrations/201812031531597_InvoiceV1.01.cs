namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceV101 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceProducts", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceProducts", "Name", c => c.String());
        }
    }
}
