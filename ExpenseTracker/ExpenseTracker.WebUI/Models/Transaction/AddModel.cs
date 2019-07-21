using System.Collections.Generic;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class AddModel : BaseEditableTransactionModel
    {
        public SelectList TemplateListForView { get; set; }
        public List<TemplateProperties> TemplateList { get; set; }
        public int TemplateId { get; set; }

        public class TemplateProperties
        {
            public int TemplateId { get; set; }
            public string TemplateName { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public int AccountId { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
