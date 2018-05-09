using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Done.Web.Areas.Administration.Models
{
    public class NewUserViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
    }
}