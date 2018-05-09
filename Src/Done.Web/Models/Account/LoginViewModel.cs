using System.ComponentModel.DataAnnotations;

namespace Done.Web.Models.Account
{
   public class LoginViewModel
    {
        [Required]
        [Display(Name = "User")]
        public string User { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
         
        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }
    }
}