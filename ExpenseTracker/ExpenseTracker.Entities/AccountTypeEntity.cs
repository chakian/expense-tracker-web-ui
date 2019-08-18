using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Entities
{
    public class AccountTypeEntity
    {
        public int AccountTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
