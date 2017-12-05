namespace Tbs16.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.Application",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        Status = c.Int(),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Note = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        RegNumber = c.String(nullable: false, maxLength: 50),
                        Year = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "app.CoordinationRequest",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SourceUser = c.String(),
                        DistanationUser = c.String(),
                        Type = c.Int(nullable: false),
                        Message = c.String(),
                        ApplicationId = c.Int(nullable: false),
                        CoordinationResponseId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("app.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("app.CoordinationResponse", t => t.CoordinationResponseId)
                .Index(t => t.ApplicationId)
                .Index(t => t.CoordinationResponseId);
            
            CreateTable(
                "app.CoordinationResponse",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        IsAgree = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "settings.Module",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "user.Role",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "user.User_Role",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        roleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "user.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.Login, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.CoordinationRequest", "CoordinationResponseId", "app.CoordinationResponse");
            DropForeignKey("app.CoordinationRequest", "ApplicationId", "app.Application");
            DropForeignKey("app.Application", "CarId", "dbo.Cars");
            DropIndex("user.User", new[] { "Login" });
            DropIndex("app.CoordinationRequest", new[] { "CoordinationResponseId" });
            DropIndex("app.CoordinationRequest", new[] { "ApplicationId" });
            DropIndex("app.Application", new[] { "CarId" });
            DropTable("user.User");
            DropTable("user.User_Role");
            DropTable("user.Role");
            DropTable("settings.Module");
            DropTable("app.CoordinationResponse");
            DropTable("app.CoordinationRequest");
            DropTable("dbo.Cars");
            DropTable("app.Application");
        }
    }
}
