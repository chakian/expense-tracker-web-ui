using ExpenseTracker.Entities.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ExpenseTracker.Entities
{
    public class UserEntity : AuditableEntity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int? ActiveBudgetId { get; set; }
        public bool EmailConfirmed { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; }
    }
}
