using System.Collections.Generic;
using Done.Web.Models.ViewModels.Pagination;

namespace Done.Web.Models.ViewModels.Goals
{
    public sealed class IndexViewModel
    {
        public int Total { get; set; }

        public IEnumerable<GoalViewModel> Goals { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}