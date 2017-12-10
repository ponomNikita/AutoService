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
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        Status = c.Int(),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Note = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        RegNumber = c.String(nullable: false, maxLength: 50),
                        Year = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "app.CoordinationRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceUser = c.String(),
                        DistanationUser = c.String(),
                        Type = c.Int(nullable: false),
                        Message = c.String(),
                        ApplicationId = c.Int(nullable: false),
                        CoordinationResponseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("app.Application", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("app.CoordinationResponse", t => t.CoordinationResponseId)
                .Index(t => t.ApplicationId)
                .Index(t => t.CoordinationResponseId);
            
            CreateTable(
                "app.CoordinationResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        IsAgree = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "settings.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "user.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "user.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("user.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("user.Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "user.Role");
            DropForeignKey("dbo.UserRoles", "User_Id", "user.User");
            DropForeignKey("app.CoordinationRequest", "CoordinationResponseId", "app.CoordinationResponse");
            DropForeignKey("app.CoordinationRequest", "ApplicationId", "app.Application");
            DropForeignKey("app.Application", "CarId", "dbo.Cars");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("user.User", new[] { "Login" });
            DropIndex("app.CoordinationRequest", new[] { "CoordinationResponseId" });
            DropIndex("app.CoordinationRequest", new[] { "ApplicationId" });
            DropIndex("app.Application", new[] { "CarId" });
            DropTable("dbo.UserRoles");
            DropTable("user.User");
            DropTable("user.Role");
            DropTable("settings.Module");
            DropTable("app.CoordinationResponse");
            DropTable("app.CoordinationRequest");
            DropTable("dbo.Cars");
            DropTable("app.Application");
        }
    }
}
