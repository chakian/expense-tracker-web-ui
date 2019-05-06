using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.User
{
    public class LoginModel : BaseModel
    {
        [Display(Name="Username")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}