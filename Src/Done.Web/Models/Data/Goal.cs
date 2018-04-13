namespace Done.Web.Models.Data
{
    public sealed class Goal
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public State State { get; set; }
    }
}