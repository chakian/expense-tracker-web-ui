using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Account
{
    public class CreateAccountModel
    {
        public int AccountId { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal StartingBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public int AccountTypeId { get; set; }
    }
}