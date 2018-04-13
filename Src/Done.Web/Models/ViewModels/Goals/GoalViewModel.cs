using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Done.Web.Models.Data;

namespace Done.Web.Models.ViewModels.Goals
{
    public sealed class GoalViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public State State { get; set; }
    }
}