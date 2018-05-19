using System;
using Microsoft.AspNetCore.Identity;

namespace Done.Domain
{
    public sealed class Goal
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public State State { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}