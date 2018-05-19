using Done.Domain;
using System.ComponentModel.DataAnnotations;

namespace Done.Web.Models.Goals
{
    public sealed class EditGoalViewModel
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // TODO: Extract view state and Use automapper
        public State State { get; set; }
    }
}
