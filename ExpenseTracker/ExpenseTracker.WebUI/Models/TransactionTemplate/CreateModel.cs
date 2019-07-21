using System;

namespace ExpenseTracker.WebUI.Models.TransactionTemplate
{
    public class CreateModel
    {
        public string TemplateName { get; set; }
        public decimal Amount { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }
        public int? SourceAccountId { get; set; }
    }
}
