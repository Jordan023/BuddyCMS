namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceV11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Description");
            DropColumn("dbo.Invoices", "InvoiceNumber");
        }
    }
}
