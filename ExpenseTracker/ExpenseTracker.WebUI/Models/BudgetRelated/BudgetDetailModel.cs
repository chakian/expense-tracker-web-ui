using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.BudgetRelated
{
    public class BudgetDetailModel : BaseModel
    {
        public int Id { get; set; }

        [Display(Name = "Adı")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Para Birimi")]
        [Required]
        public int CurrencyId { get; set; }

        [Display(Name = "Para Birimi")]
        public string CurrencyLongName { get; set; }
    }
}