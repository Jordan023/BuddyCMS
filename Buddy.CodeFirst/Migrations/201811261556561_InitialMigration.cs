namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetName = c.String(maxLength: 50),
                        StreetNumber = c.String(maxLength: 10),
                        Postal = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsoCode = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ModuleCode = c.String(maxLength: 20),
                        FunctionName = c.String(maxLength: 50),
                        Timestamp = c.DateTime(nullable: false),
                        Message = c.String(),
                        LogType_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogTypes", t => t.LogType_Id)
                .Index(t => t.LogType_Id);
            
            CreateTable(
                "dbo.LogTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        PasswordSalt = c.String(maxLength: 50),
                        PasswordHash = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        EmailAddress = c.String(maxLength: 200),
                        PhoneNumber = c.String(maxLength: 20),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Logs", "LogType_Id", "dbo.LogTypes");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropIndex("dbo.Logs", new[] { "LogType_Id" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Modules");
            DropTable("dbo.LogTypes");
            DropTable("dbo.Logs");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
