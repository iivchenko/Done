using System.Data.Entity;

namespace Done.Web.Models
{
    // TODO: Remove the initializer. Provide new db initializer with starter kit: welcom task, basic tags (status.inprogress status.done, priority.low, priority.middle) etc.
    public sealed class TasksInitializer : DropCreateDatabaseAlways<GoalsContext>
    {
        protected override void Seed(GoalsContext db)
        {
            db.Tasks.Add(new Goal {Name = "Task 1", State = State.Open});
            db.Tasks.Add(new Goal {Name = "Task 2", State = State.Open});

            base.Seed(db);
        }
    }
}