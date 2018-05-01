using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Done.Domain;

namespace Done.Web.Models.Goals
{
    public sealed class GoalViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public State State { get; set; }
    }
}