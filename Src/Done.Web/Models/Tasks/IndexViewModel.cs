using System.Collections.Generic;
using Done.Web.Models.Pagination;

namespace Done.Web.Models.Tasks
{
    public sealed class IndexViewModel
    {
        public int Total { get; set; }

        public IEnumerable<Goal> Tasks { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}