namespace ExpenseTracker.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            DropForeignKey("dbo.Users", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Categories", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlanCategories", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlans", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.BudgetPlans", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Accounts", "InsertUserId", "dbo.Users");
            DropForeignKey("dbo.Accounts", "UpdateUserId", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "InsertUserId" });
            DropIndex("dbo.Accounts", new[] { "UpdateUserId" });
            DropIndex("dbo.Budgets", new[] { "InsertUserId" });
            DropIndex("dbo.Budgets", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "UpdateUserId" });
            DropIndex("dbo.Categories", new[] { "InsertUserId" });
            DropIndex("dbo.Categories", new[] { "UpdateUserId" });
            DropIndex("dbo.AspNetUsers", new[] { "InsertUserId" });
            DropIndex("dbo.AspNetUsers", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "UserId" });
            DropIndex("dbo.BudgetUsers", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "UpdateUserId" });
            DropIndex("dbo.Transactions", new[] { "InsertUserId" });
            DropIndex("dbo.Transactions", new[] { "UpdateUserId" });
            DropPrimaryKey("dbo.AspNetUsers");
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Accounts", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Accounts", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Budgets", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Budgets", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BudgetPlans", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BudgetPlans", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BudgetPlanCategories", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BudgetPlanCategories", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Categories", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.BudgetUsers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BudgetUsers", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BudgetUsers", "UpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Transactions", "InsertUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Transactions", "UpdateUserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            CreateIndex("dbo.Accounts", "InsertUserId");
            CreateIndex("dbo.Accounts", "UpdateUserId");
            CreateIndex("dbo.Budgets", "InsertUserId");
            CreateIndex("dbo.Budgets", "UpdateUserId");
            CreateIndex("dbo.BudgetPlans", "InsertUserId");
            CreateIndex("dbo.BudgetPlans", "UpdateUserId");
            CreateIndex("dbo.BudgetPlanCategories", "InsertUserId");
            CreateIndex("dbo.BudgetPlanCategories", "UpdateUserId");
            CreateIndex("dbo.Categories", "InsertUserId");
            CreateIndex("dbo.Categories", "UpdateUserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Transactions", "InsertUserId");
            CreateIndex("dbo.Transactions", "UpdateUserId");
            CreateIndex("dbo.BudgetUsers", "UserId");
            CreateIndex("dbo.BudgetUsers", "InsertUserId");
            CreateIndex("dbo.BudgetUsers", "UpdateUserId");
            AddForeignKey("dbo.Categories", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Categories", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetPlanCategories", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetPlanCategories", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetPlans", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetPlans", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetUsers", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetUsers", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BudgetUsers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Budgets", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Budgets", "UpdateUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Accounts", "InsertUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Accounts", "UpdateUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "UserId");
            DropColumn("dbo.AspNetUsers", "InsertUserId");
            DropColumn("dbo.AspNetUsers", "InsertTime");
            DropColumn("dbo.AspNetUsers", "UpdateUserId");
            DropColumn("dbo.AspNetUsers", "UpdateTime");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "UpdateUserId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "InsertTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "InsertUserId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Accounts", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accounts", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Budgets", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Budgets", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetUsers", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetUsers", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetPlans", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetPlans", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetPlanCategories", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BudgetPlanCategories", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "UpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "InsertUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BudgetUsers", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetUsers", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "UpdateUserId" });
            DropIndex("dbo.Transactions", new[] { "InsertUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Categories", new[] { "UpdateUserId" });
            DropIndex("dbo.Categories", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlanCategories", new[] { "InsertUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "UpdateUserId" });
            DropIndex("dbo.BudgetPlans", new[] { "InsertUserId" });
            DropIndex("dbo.Budgets", new[] { "UpdateUserId" });
            DropIndex("dbo.Budgets", new[] { "InsertUserId" });
            DropIndex("dbo.Accounts", new[] { "UpdateUserId" });
            DropIndex("dbo.Accounts", new[] { "InsertUserId" });
            DropPrimaryKey("dbo.AspNetUsers");
            AlterColumn("dbo.Transactions", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.Transactions", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetUsers", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.BudgetUsers", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
            AlterColumn("dbo.Categories", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.Categories", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetPlanCategories", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.BudgetPlanCategories", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BudgetPlans", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.BudgetPlans", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Budgets", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.Budgets", "InsertUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Accounts", "UpdateUserId", c => c.Int());
            AlterColumn("dbo.Accounts", "InsertUserId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Id");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddPrimaryKey("dbo.AspNetUsers", "UserId");
            CreateIndex("dbo.Transactions", "UpdateUserId");
            CreateIndex("dbo.Transactions", "InsertUserId");
            CreateIndex("dbo.BudgetUsers", "UpdateUserId");
            CreateIndex("dbo.BudgetUsers", "InsertUserId");
            CreateIndex("dbo.BudgetUsers", "UserId");
            CreateIndex("dbo.AspNetUsers", "UpdateUserId");
            CreateIndex("dbo.AspNetUsers", "InsertUserId");
            CreateIndex("dbo.Categories", "UpdateUserId");
            CreateIndex("dbo.Categories", "InsertUserId");
            CreateIndex("dbo.BudgetPlanCategories", "UpdateUserId");
            CreateIndex("dbo.BudgetPlanCategories", "InsertUserId");
            CreateIndex("dbo.BudgetPlans", "UpdateUserId");
            CreateIndex("dbo.BudgetPlans", "InsertUserId");
            CreateIndex("dbo.Budgets", "UpdateUserId");
            CreateIndex("dbo.Budgets", "InsertUserId");
            CreateIndex("dbo.Accounts", "UpdateUserId");
            CreateIndex("dbo.Accounts", "InsertUserId");
            AddForeignKey("dbo.Accounts", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Accounts", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Budgets", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Budgets", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetPlans", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetPlans", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetPlanCategories", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetPlanCategories", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Categories", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Transactions", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Transactions", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Categories", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetUsers", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetUsers", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.BudgetUsers", "InsertUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Users", "UpdateUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Users", "InsertUserId", "dbo.Users", "UserId");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
        }
    }
}
