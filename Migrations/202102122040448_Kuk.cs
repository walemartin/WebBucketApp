namespace WebBucketApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kuk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ClientDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Gender = c.Int(),
                        HomeAddress = c.String(maxLength: 250),
                        CiientEmail = c.String(),
                        PhoneNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(),
                        Branch = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyTokens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Branch = c.String(),
                        CompToken = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CompanyTokenId = c.Int(),
                        Address = c.String(maxLength: 250),
                        CompToken = c.String(),
                        PhoneNo = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyTokens", t => t.CompanyTokenId)
                .Index(t => t.CompanyTokenId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.EditUserViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CompanyTokenId = c.Int(),
                        Address = c.String(maxLength: 250),
                        CompToken = c.String(),
                        PhoneNo = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyTokens", t => t.CompanyTokenId)
                .Index(t => t.CompanyTokenId);
            
            CreateTable(
                "dbo.RegisterViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CompanyTokenId = c.Int(),
                        Address = c.String(maxLength: 250),
                        CompToken = c.String(),
                        PhoneNo = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CompanyTokens", t => t.CompanyTokenId)
                .Index(t => t.CompanyTokenId);
            
            CreateTable(
                "dbo.FetchARecordAuditTrails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientPhone = c.String(),
                        ClientEmail = c.String(),
                        CreatedBy = c.String(),
                        UserIPAddress = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ImageDirs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(),
                        ContractNo = c.String(),
                        DocPath = c.String(),
                        LaundryManager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LaundryManagers", t => t.LaundryManager_ID)
                .Index(t => t.LaundryManager_ID);
            
            CreateTable(
                "dbo.LaundryManagers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(),
                        ContractNo = c.String(),
                        FullName = c.String(),
                        ShirtNo = c.Byte(nullable: false),
                        TrouserNo = c.Byte(nullable: false),
                        JeanNo = c.Byte(nullable: false),
                        AgbadaCompleteNo = c.Byte(nullable: false),
                        ClientRemark = c.String(maxLength: 400),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(),
                        Branch = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LaundryPayments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(),
                        ContractNo = c.String(),
                        ReceivedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        Branch = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.String(),
                        Person = c.String(),
                        Item = c.String(),
                        Units = c.String(),
                        UnitCost = c.String(),
                        Total = c.String(),
                        AddedOn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UploadAuditTrails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        file = c.String(),
                        CreatedBy = c.String(),
                        UserIPAddress = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WashWorkFlows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientNo = c.String(),
                        ContractNo = c.String(),
                        WorkStatus = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Email = c.String(),
                        Branch = c.String(),
                        TrxnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ImageDirs", "LaundryManager_ID", "dbo.LaundryManagers");
            DropForeignKey("dbo.RegisterViewModels", "CompanyTokenId", "dbo.CompanyTokens");
            DropForeignKey("dbo.EditUserViewModels", "CompanyTokenId", "dbo.CompanyTokens");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CompanyTokenId", "dbo.CompanyTokens");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ImageDirs", new[] { "LaundryManager_ID" });
            DropIndex("dbo.RegisterViewModels", new[] { "CompanyTokenId" });
            DropIndex("dbo.EditUserViewModels", new[] { "CompanyTokenId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyTokenId" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.WashWorkFlows");
            DropTable("dbo.UploadAuditTrails");
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LaundryPayments");
            DropTable("dbo.LaundryManagers");
            DropTable("dbo.ImageDirs");
            DropTable("dbo.FetchARecordAuditTrails");
            DropTable("dbo.RegisterViewModels");
            DropTable("dbo.EditUserViewModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CompanyTokens");
            DropTable("dbo.ClientDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
