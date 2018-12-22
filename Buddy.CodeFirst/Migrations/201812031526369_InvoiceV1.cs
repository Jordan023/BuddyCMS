namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PriceExclVat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Int(nullable: false),
                        Invoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .Index(t => t.Invoice_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Address_Id = c.Int(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Users", t => t.Customer_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceProducts", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Customer_Id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Invoices", new[] { "Customer_Id" });
            DropIndex("dbo.Invoices", new[] { "Address_Id" });
            DropIndex("dbo.InvoiceProducts", new[] { "Invoice_Id" });
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceProducts");
        }
    }
}
