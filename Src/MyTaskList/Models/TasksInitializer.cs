using System.Data.Entity;

namespace MyTaskList.Models
{
    // TODO: Remove the initializer. Provide new db initializer with starter kit: welcom task, basic tags (status.inprogress status.done, priority.low, priority.middle) etc.
    public sealed class TasksInitializer : DropCreateDatabaseAlways<TasksContext>
    {
        protected override void Seed(TasksContext db)
        {
            db.Tasks.Add(new TaskItem {Name = "Task 1", Status = TaskItemStatus.Done});
            db.Tasks.Add(new TaskItem {Name = "Task 2", Status = TaskItemStatus.InProgress});

            base.Seed(db);
        }
    }
}