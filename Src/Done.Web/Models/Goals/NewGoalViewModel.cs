using System.ComponentModel.DataAnnotations;

namespace Done.Web.Models.Goals
{
    public sealed class NewGoalViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
