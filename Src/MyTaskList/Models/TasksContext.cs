using System.Data.Entity;

namespace MyTaskList.Models
{
    public sealed class TasksContext : DbContext
    {
        public TasksContext()
            : base("Default")
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}