using System.Data.Entity;

namespace Done.Web.Models
{
    // TODO: Remove the initializer. Provide new db initializer with starter kit: welcom task, basic tags (status.inprogress status.done, priority.low, priority.middle) etc.
    public sealed class GoalsInitializer : DropCreateDatabaseAlways<GoalsContext>
    {
        protected override void Seed(GoalsContext db)
        {
            db.Goals.Add(new Goal {Name = "Goal 1", State = State.Open});
            db.Goals.Add(new Goal {Name = "Goal 2", State = State.Open});

            base.Seed(db);
        }
    }
}