namespace DataStruct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswersId = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswersId)
                .ForeignKey("dbo.Questions", t => t.QuestionsId, cascadeDelete: true)
                .Index(t => t.QuestionsId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionsId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        ExamsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionsId)
                .ForeignKey("dbo.Exams", t => t.ExamsId, cascadeDelete: true)
                .Index(t => t.ExamsId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ExamsId = c.Int(nullable: false, identity: true),
                        ExamType = c.Int(nullable: false),
                        ExamName = c.String(),
                        LectureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamsId)
                .ForeignKey("dbo.Lectures", t => t.LectureId, cascadeDelete: true)
                .Index(t => t.LectureId);
            
            CreateTable(
                "dbo.ExamStats",
                c => new
                    {
                        ExamStatsId = c.Int(nullable: false, identity: true),
                        ExamScore = c.Double(nullable: false),
                        ExamDate = c.DateTime(nullable: false),
                        CorrectAnswers = c.Int(nullable: false),
                        ExamsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamStatsId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Exams", t => t.ExamsId, cascadeDelete: true)
                .Index(t => t.ExamsId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
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
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderCreated = c.DateTime(nullable: false),
                        Email = c.String(),
                        Status = c.String(),
                        Address = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        ChargesId = c.Int(nullable: false, identity: true),
                        Amount = c.Long(nullable: false),
                        AuthorizationCode = c.String(),
                        ChargeDate = c.DateTime(nullable: false),
                        Currency = c.String(),
                        ReceiptEmail = c.String(),
                        ReceiptNumber = c.String(),
                        OrderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PaymentIntentId = c.Int(nullable: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ChargesId)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        Amount = c.Long(nullable: false),
                        Quantity = c.Long(nullable: false),
                        Currency = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Images = c.String(),
                        Description = c.String(maxLength: 200),
                        OrderDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetailsId, cascadeDelete: true)
                .Index(t => t.OrderDetailsId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        LecturesId = c.Int(nullable: false, identity: true),
                        Lecture = c.String(),
                        LectureName = c.String(),
                    })
                .PrimaryKey(t => t.LecturesId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Loggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Exception = c.String(),
                        UserName = c.String(),
                        ExceptionDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Answers", "QuestionsId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "ExamsId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "LectureId", "dbo.Lectures");
            DropForeignKey("dbo.ExamStats", "ExamsId", "dbo.Exams");
            DropForeignKey("dbo.ExamStats", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Products", "OrderDetailsId", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Charges", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Charges", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "OrderDetailsId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Charges", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Charges", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.ExamStats", new[] { "UserId" });
            DropIndex("dbo.ExamStats", new[] { "ExamsId" });
            DropIndex("dbo.Exams", new[] { "LectureId" });
            DropIndex("dbo.Questions", new[] { "ExamsId" });
            DropIndex("dbo.Answers", new[] { "QuestionsId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Loggers");
            DropTable("dbo.Carts");
            DropTable("dbo.Lectures");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Charges");
            DropTable("dbo.Orders");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.ExamStats");
            DropTable("dbo.Exams");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
