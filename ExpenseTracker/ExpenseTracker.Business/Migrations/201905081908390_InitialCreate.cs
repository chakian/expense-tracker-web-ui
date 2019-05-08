namespace ExpenseTracker.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountTypeId = c.Int(nullable: false),
                        BudgetId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.AccountTypeId)
                .Index(t => t.BudgetId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CurrencyId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.CurrencyId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.BudgetUsers",
                c => new
                    {
                        BudgetUserId = c.Int(nullable: false, identity: true),
                        BudgetId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        User_UserId = c.Int(),
                        User_UserId1 = c.Int(),
                    })
                .PrimaryKey(t => t.BudgetUserId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.BudgetId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        InsertUserId = c.Int(),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.InsertUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BudgetId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.BudgetId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.BudgetPlanCategories",
                c => new
                    {
                        BudgetPlanCategoryId = c.Int(nullable: false, identity: true),
                        PlannedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetPlanId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetPlanCategoryId)
                .ForeignKey("dbo.BudgetPlans", t => t.BudgetPlanId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.BudgetPlanId)
                .Index(t => t.CategoryId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.BudgetPlans",
                c => new
                    {
                        BudgetPlanId = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        BudgetId = c.Int(nullable: false),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetPlanId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.BudgetId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        CategoryId = c.Int(),
                        SourceAccountId = c.Int(nullable: false),
                        TargetAccountId = c.Int(),
                        InsertUserId = c.Int(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.InsertUserId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.SourceAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.TargetAccountId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.CategoryId)
                .Index(t => t.SourceAccountId)
                .Index(t => t.TargetAccountId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "TargetAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "SourceAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Accounts", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Accounts", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Categories", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BudgetPlans", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlans", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "BudgetPlanId", "dbo.BudgetPlans");
            DropForeignKey("dbo.BudgetPlans", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Categories", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.BudgetUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Accounts", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Accounts", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.Transactions", new[] { "UpdateUserId" });
            DropIndex("dbo.Transactions", new[] { "InsertUserId" });
            DropIndex("dbo.Transactions", new[] { "TargetAccountId" });
            DropIndex("dbo.Transactions", new[] { "SourceAccountId" });
            DropIndex("dbo.Transactions", new[] { "CategoryId" });
            DropIndex("dbo.BudgetPlans", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "BudgetId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "CategoryId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "BudgetPlanId" });
            DropIndex("dbo.Categories", new[] { "UpdateUserId" });
            DropIndex("dbo.Categories", new[] { "InsertUserId" });
            DropIndex("dbo.Categories", new[] { "BudgetId" });
            DropIndex("dbo.Users", new[] { "UpdateUserId" });
            DropIndex("dbo.Users", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "User_UserId1" });
            DropIndex("dbo.BudgetUsers", new[] { "User_UserId" });
            DropIndex("dbo.BudgetUsers", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "BudgetId" });
            DropIndex("dbo.Budgets", new[] { "UpdateUserId" });
            DropIndex("dbo.Budgets", new[] { "InsertUserId" });
            DropIndex("dbo.Budgets", new[] { "CurrencyId" });
            DropIndex("dbo.Accounts", new[] { "UpdateUserId" });
            DropIndex("dbo.Accounts", new[] { "InsertUserId" });
            DropIndex("dbo.Accounts", new[] { "BudgetId" });
            DropIndex("dbo.Accounts", new[] { "AccountTypeId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Currencies");
            DropTable("dbo.BudgetPlans");
            DropTable("dbo.BudgetPlanCategories");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
            DropTable("dbo.BudgetUsers");
            DropTable("dbo.Budgets");
            DropTable("dbo.AccountTypes");
            DropTable("dbo.Accounts");
        }
    }
}
