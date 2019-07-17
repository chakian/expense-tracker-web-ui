namespace ExpenseTracker.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionTemplate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        CategoryId = c.Int(),
                        SourceAccountId = c.Int(),
                        TargetAccountId = c.Int(),
                        BudgetId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        InsertUserId = c.String(nullable: false, maxLength: 128),
                        InsertTime = c.DateTime(nullable: false),
                        UpdateUserId = c.String(maxLength: 128),
                        UpdateTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        SourceAccount_AccountId = c.Int(),
                        TargetAccount_AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.InsertUserId)
                .ForeignKey("dbo.Accounts", t => t.SourceAccount_AccountId)
                .ForeignKey("dbo.Accounts", t => t.TargetAccount_AccountId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.BudgetId)
                .Index(t => t.UserId)
                .Index(t => t.InsertUserId)
                .Index(t => t.UpdateUserId)
                .Index(t => t.SourceAccount_AccountId)
                .Index(t => t.TargetAccount_AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTemplates", "UserId", "dbo.Users");
            DropForeignKey("dbo.TransactionTemplates", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionTemplates", "TargetAccount_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.TransactionTemplates", "SourceAccount_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.TransactionTemplates", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionTemplates", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TransactionTemplates", "BudgetId", "dbo.Budgets");
            DropIndex("dbo.TransactionTemplates", new[] { "TargetAccount_AccountId" });
            DropIndex("dbo.TransactionTemplates", new[] { "SourceAccount_AccountId" });
            DropIndex("dbo.TransactionTemplates", new[] { "UpdateUserId" });
            DropIndex("dbo.TransactionTemplates", new[] { "InsertUserId" });
            DropIndex("dbo.TransactionTemplates", new[] { "UserId" });
            DropIndex("dbo.TransactionTemplates", new[] { "BudgetId" });
            DropIndex("dbo.TransactionTemplates", new[] { "CategoryId" });
            DropTable("dbo.TransactionTemplates");
        }
    }
}
