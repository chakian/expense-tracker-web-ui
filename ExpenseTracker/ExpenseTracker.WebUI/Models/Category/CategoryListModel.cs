using ExpenseTracker.Entities;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Category
{
    public class CategoryListModel : BaseModel
    {
        public IEnumerable<CategoryEntity> Categories { get; set; }
    }
}