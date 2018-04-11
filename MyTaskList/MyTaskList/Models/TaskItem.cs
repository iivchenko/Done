namespace MyTaskList.Models
{
    public sealed class TaskItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}