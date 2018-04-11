using MyTaskList.Models.Pagination;
using System.Collections.Generic;

namespace MyTaskList.Models.Tasks
{
    public sealed class IndexViewModel
    {
        public int Total { get; set; }

        public IEnumerable<TaskItem> Tasks { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}