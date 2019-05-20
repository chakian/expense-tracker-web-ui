using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.BudgetRelated
{
    public class BudgetCreateModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Para Birimi")]
        public int CurrencyId { get; set; }
    }
}