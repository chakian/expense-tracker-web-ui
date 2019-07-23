namespace ExpenseTracker.Persistence.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Category_IsIncome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsIncomeCategory", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsIncomeCategory");
        }
    }
}
