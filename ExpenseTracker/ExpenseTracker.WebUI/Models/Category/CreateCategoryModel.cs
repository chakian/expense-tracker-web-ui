namespace ExpenseTracker.WebUI.Models.Category
{
    public class CreateCategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncomeCategory { get; set; }
        public int BudgetId { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
