namespace Buddy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HoursV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project = c.String(maxLength: 100),
                        Task = c.String(maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hours", "User_Id", "dbo.Users");
            DropIndex("dbo.Hours", new[] { "User_Id" });
            DropTable("dbo.Hours");
        }
    }
}
