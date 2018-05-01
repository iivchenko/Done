using System.Collections.Generic;
using Done.Core.Web.Pagination;

namespace Done.Web.Models.Goals
{
    public sealed class IndexViewModel
    {
        public int Total { get; set; }

        public IEnumerable<GoalViewModel> Goals { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}